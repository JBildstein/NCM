using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using ColorManager.Conversion;

namespace ColorManager
{
    public partial class ColorConverter
    {
        protected sealed class ConversionCreator_Color : ConversionCreator
        {
            #region Variables

            /// <summary>
            /// Stores all conversion paths per color type 
            /// </summary>
            private Dictionary<Type, List<ConversionPath>> Paths;
            /// <summary>
            /// The chromatic adaption method
            /// </summary>
            private ChromaticAdaption CA;

            #endregion

            #region Init

            /// <summary>
            /// Creates a new instance of the <see cref="ConversionCreator_Color"/> class
            /// </summary>
            /// <param name="CMIL">The ILGenerator for the conversion method</param>
            /// <param name="data">The conversion data</param>
            /// <param name="inColor">The input color</param>
            /// <param name="outColor">The output color</param>
            public ConversionCreator_Color(ILGenerator CMIL, ConversionData data, Color inColor, Color outColor)
                : base(CMIL, data, inColor, outColor)
            { }

            /// <summary>
            /// Creates a new instance of the <see cref="ConversionCreator_Color"/> class
            /// </summary>
            /// <param name="parent">The parent <see cref="ConversionCreator"/></param>
            /// <param name="isFirst">States if the input color is the first color</param>
            /// <param name="isLast">States if the output color is the last color</param>
            /// <param name="inColor">The input color</param>
            /// <param name="outColor">The output color</param>
            public ConversionCreator_Color(ConversionCreator parent, Color inColor, Color outColor, bool isFirst, bool isLast)
                : base(parent, inColor, outColor, isFirst, isLast)
            { }
            
            public override void SetConversionMethod()
            {
                Type inType = InColor.GetType();
                Type outType = OutColor.GetType();
                int inChCount = InColor.ChannelCount;
                int outChCount = OutColor.ChannelCount;
                bool doCA = Data.IsCARequired;

                SetPathsDict();
                IConversionCommand[] cmds;

                if (inType == outType)
                {
                    if (!doCA)
                    {
                        if (inChCount == outChCount) cmds = new IConversionCommand[] { new CC_Assign(InColor.ChannelCount) };
                        else cmds = FindPath(inType);
                    }
                    else
                    {
                        CA = ColorConverter.ChromaticAdaptions.FirstOrDefault(t => t.ColorType == inType);
                        if (CA != null) cmds = new IConversionCommand[] { new CC_ExecuteMethod(CA.Method) };
                        else cmds = FindPathCA(inType, outType);
                    }
                }
                else if (doCA) cmds = FindPathCA(inType, outType);
                else cmds = FindPath(inType, outType);

                if (cmds == null) throw new ConversionNotFoundException(inType, outType);
                if (CA != null) Data.SetCAData(CA);
                
                var outCmds = new List<IConversionCommand>();
                FindCommands(cmds, outCmds);
                WriteMethod(outCmds);
                WriteRangeCheck(OutColor);
            }

            #endregion

            #region Find Path

            private void SetPathsDict()
            {
                Paths = new Dictionary<Type, List<ConversionPath>>();
                foreach (var p in ColorConverter.ConversionPaths)
                {
                    if (Paths.ContainsKey(p.From)) Paths[p.From].Add(p);
                    else Paths.Add(p.From, new List<ConversionPath>() { p });
                }
            }

            private IConversionCommand[] FindPathCA(Type From, Type To)
            {
                List<IConversionCommand> CAPath = null;
                foreach (var ca in ColorConverter.ChromaticAdaptions)
                {
                    IConversionCommand[] p1, p2;

                    if (ca.ColorType != From) p1 = FindPath(From, ca.ColorType);
                    else p1 = new IConversionCommand[0];

                    if (ca.ColorType != To) p2 = FindPath(ca.ColorType, To);
                    else p2 = new IConversionCommand[0];
                    
                    if (p1 != null && p2 != null && (CAPath == null || CAPath.Count < p1.Length + p2.Length + 1))
                    {
                        CA = ca;
                        CAPath = new List<IConversionCommand>();
                        CAPath.AddRange(p1);
                        CAPath.Add(new CC_ExecuteMethod(ca.Method));
                        CAPath.AddRange(p2);
                    }
                }

                if (CAPath != null) return CAPath.ToArray();
                else return null;
            }

            private IConversionCommand[] FindPath(Type From, Type To)
            {
                //Find all nodes
                var path = FindPathSub(From, To, From, new List<Type>());

                if (path != null)
                {
                    //Split up nodes into single paths
                    List<ConversionPath[]> otp = new List<ConversionPath[]>();
                    SplitPaths(path, new List<ConversionPath>(), otp);
                    return GetCommands(otp.OrderBy(t => t.Length).First());
                }
                else return null;
            }

            private IConversionCommand[] FindPath(Type tp)
            {
                List<ConversionPath> to;
                if (Paths.TryGetValue(tp, out to))
                {
                    foreach (var item in to)
                    {
                        List<ConversionPath> back;
                        if (Paths.TryGetValue(tp, out back))
                        {
                            var p = back.FirstOrDefault(t => t.To == tp);
                            if (p != null) return GetCommands(new ConversionPath[] { item, p });
                        }
                    }
                }
                return null;
            }

            private List<SubPath> FindPathSub(Type From, Type To, Type prevFrom, List<Type> usedTypes)
            {
                if (!Paths.ContainsKey(From)) return null;

                if (usedTypes.Contains(From)) return null;
                else usedTypes.Add(From);

                var possible = Paths[From].Where(t => t.To != prevFrom);
                List<SubPath> cpath = new List<SubPath>();

                foreach (var item in possible)
                {
                    if (item.To == To) cpath.Add(new SubPath(item));
                    else
                    {
                        var npath = FindPathSub(item.To, To, From, usedTypes);
                        if (npath != null) cpath.Add(new SubPath(item, npath));
                    }
                }

                usedTypes.Remove(From);

                if (cpath.Count > 0) return cpath;
                else return null;
            }

            private void SplitPaths(List<SubPath> paths, List<ConversionPath> curPath, List<ConversionPath[]> outp)
            {
                for (int i = 0; i < paths.Count; i++)
                {
                    curPath.Add(paths[i].Path);

                    if (paths[i].Sub == null) outp.Add(curPath.ToArray());
                    else SplitPaths(paths[i].Sub, curPath, outp);

                    int idx = curPath.FindLastIndex(t => t == paths[i].Path);
                    curPath.RemoveAt(idx);
                }
            }

            private IConversionCommand[] GetCommands(IEnumerable<ConversionPath> path)
            {
                if (path == null) return null;

                var cmds = new List<IConversionCommand>();
                foreach (var p in path) cmds.AddRange(p.Commands);
                return cmds.ToArray();
            }

            private sealed class SubPath
            {
                public ConversionPath Path;
                public List<SubPath> Sub;

                public SubPath(ConversionPath Path)
                {
                    this.Path = Path;
                }

                public SubPath(ConversionPath Path, List<SubPath> Sub)
                {
                    this.Path = Path;
                    this.Sub = Sub;
                }
            }

            #endregion

            #region Find Commands

            /// <summary>
            /// Resolves all conversion commands to only be of type <see cref="CC_Assign"/>
            /// and <see cref="CC_ExecuteMethod"/>
            /// </summary>
            /// <param name="inCmds">The input commands</param>
            /// <param name="outCmds">The resolved output commands</param>
            private void FindCommands(IEnumerable<IConversionCommand> inCmds, List<IConversionCommand> outCmds)
            {
                foreach (var cmd in inCmds) FindCommand(cmd, outCmds);
            }

            /// <summary>
            /// Resolves a conversion command into a set of commands of type
            /// <see cref="CC_Assign"/> or <see cref="CC_ExecuteMethod"/>
            /// </summary>
            /// <param name="cmd">The command to resolve</param>
            /// <param name="outCmds">The resolved output commands</param>
            private void FindCommand(IConversionCommand cmd, List<IConversionCommand> outCmds)
            {
                if (cmd is CC_Assign || cmd is CC_ExecuteMethod) outCmds.Add(cmd);
                else
                {
                    var condition = cmd as CC_Condition;
                    if (condition != null) { FindCommand(condition, outCmds); return; }

                    var convert = cmd as CC_Convert;
                    if (convert != null) { FindConvertTo(convert.inColor, convert.outColor, outCmds); return; }
                }
            }

            /// <summary>
            /// Resolves a <see cref="CC_Condition"/> command into a set of commands of type
            /// <see cref="CC_Assign"/> or <see cref="CC_ExecuteMethod"/>
            /// </summary>
            /// <param name="cmd">The command to resolve</param>
            /// <param name="outCmds">The resolved output commands</param>
            private void FindCommand(CC_Condition condition, List<IConversionCommand> outCmds)
            {
                if (condition.condition(Data)) foreach (var cmd in condition.IfCommands) FindCommand(cmd, outCmds);
                else if (condition.ElseCommands != null) foreach (var cmd in condition.ElseCommands) FindCommand(cmd, outCmds);
            }

            /// <summary>
            /// Finds a path between two colors and resolves it into a set of commands of type
            /// <see cref="CC_Assign"/> or <see cref="CC_ExecuteMethod"/>
            /// </summary>
            /// <param name="inColor">The input color</param>
            /// <param name="outColor">The output color</param>
            /// <param name="outCmds">The resolved output commands</param>
            private void FindConvertTo(Type inColor, Type outColor, List<IConversionCommand> outCmds)
            {
                IConversionCommand[] cmdList = FindPath(inColor, outColor);
                FindCommands(cmdList, outCmds);
            }

            #endregion

            #region Write IL code

            /// <summary>
            /// Writes the IL code for a list of conversion commands
            /// </summary>
            /// <param name="cmds">The conversion commands</param>
            private void WriteMethod(List<IConversionCommand> cmds)
            {
                for (int i = 0; i < cmds.Count; i++)
                {
                    if (i != 0 && IsFirst) IsFirst = false;
                    if (i == cmds.Count - 1) IsLast = true;

                    var assign = cmds[i] as CC_Assign;
                    if (assign != null)
                    {
                        WriteAssign(assign.channels);
                        continue;
                    }

                    var execute = cmds[i] as CC_ExecuteMethod;
                    if (execute != null)
                    {
                        WriteMethod(execute);
                        continue;
                    }
                }
            }

            /// <summary>
            /// Writes the IL code to call a method
            /// </summary>
            /// <param name="cc">The conversion command with the method</param>
            private void WriteMethod(CC_ExecuteMethod cc)
            {
                MethodInfo m;
                if (cc.methodC != null) m = cc.methodC.Method;
                else if (cc.methodTTo != null) m = cc.methodTTo.Method;
                else if (cc.methodT != null) m = cc.methodT.Method;
                else return;

                WriteMethodCall(m);
            }

            #endregion
        }
    }
}
