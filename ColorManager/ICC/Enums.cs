#pragma warning disable 1591

namespace ColorManager.ICC
{
    /// <summary>
    /// Profile Class Name
    /// </summary>
    public enum ProfileClassName : uint
    {
        InputDevice = 0x73636E72,       //scnr
        DisplayDevice = 0x6D6E7472,     //mntr
        OutputDevice = 0x70727472,      //prtr
        DeviceLink = 0x6C696E6B,        //link
        ColorSpace = 0x73706163,        //spac
        Abstract = 0x61627374,          //abst
        NamedColor = 0x6E6D636C,        //nmcl
    }

    /// <summary>
    /// Color Space Type
    /// </summary>
    public enum ColorSpaceType : uint
    {
        CIEXYZ = 0x58595A20,        //XYZ
        CIELAB = 0x4C616220,        //Lab
        CIELUV = 0x4C757620,        //Luv
        YCbCr = 0x59436272,         //YCbr
        CIEYxy = 0x59787920,        //Yxy
        RGB = 0x52474220,           //RGB
        Gray = 0x47524159,          //GRAY
        HSV = 0x48535620,           //HSV
        HLS = 0x484C5320,           //HLS
        CMYK = 0x434D594B,          //CMYK
        CMY = 0x434D5920,           //CMY
        Color2 = 0x32434C52,        //2CLR
        Color3 = 0x33434C52,        //3CLR
        Color4 = 0x34434C52,        //4CLR
        Color5 = 0x35434C52,        //5CLR
        Color6 = 0x36434C52,        //6CLR
        Color7 = 0x37434C52,        //7CLR
        Color8 = 0x38434C52,        //8CLR
        Color9 = 0x39434C52,        //9CLR
        Color10 = 0x41434C52,       //ACLR
        Color11 = 0x42434C52,       //BCLR
        Color12 = 0x43434C52,       //CCLR
        Color13 = 0x44434C52,       //DCLR
        Color14 = 0x45434C52,       //ECLR
        Color15 = 0x46434C52,       //FCLR
    }

    /// <summary>
    /// Primary Platform Type
    /// </summary>
    public enum PrimaryPlatformType : uint
    {
        NotIdentified = 0x00000000,
        AppleComputerInc = 0x4150504C,          //APPL
        MicrosoftCorporation = 0x4D534654,      //MSFT
        SiliconGraphicsInc = 0x53474920,        //SGI
        SunMicrosystemsInc = 0x53554E57,        //SUNW
    }

    /// <summary>
    /// Opacity Attribute
    /// </summary>
    public enum OpacityAttribute
    {
        Reflective = 0,
        Transparency = 1,
    }

    /// <summary>
    /// Reflectivity Attribute
    /// </summary>
    public enum ReflectivityAttribute
    {
        Glossy = 0,
        Matte = 1,
    }

    /// <summary>
    /// Polarity Attribute
    /// </summary>
    public enum PolarityAttribute
    {
        Positive = 0,
        Negative = 1,
    }

    /// <summary>
    /// Chroma Attribute
    /// </summary>
    public enum ChromaAttribute
    {
        Color = 0,
        BlackWhite = 1,
    }

    /// <summary>
    /// Rendering Intent
    /// </summary>
    public enum RenderingIntent : uint
    {
        Perceptual = 0,
        MediaRelativeColorimetric = 1,
        Saturation = 2,
        AbsoluteColorimetric = 3,
    }

    /// <summary>
    /// Tag Signature
    /// </summary>
    public enum TagSignature : uint
    {
        Unknown,

        AToB0 = 0x41324230,                             //A2B0
        AToB1 = 0x41324231,                             //A2B1
        AToB2 = 0x41324232,                             //A2B2
        BlueMatrixColumn = 0x6258595A,                  //bXYZ
        BlueTRC = 0x62545243,                           //bTRC
        BToA0 = 0x42324130,                             //B2A0
        BToA1 = 0x42324131,                             //B2A1
        BToA2 = 0x42324132,                             //B2A2
        BToD0 = 0x42324430,                             //B2D0
        BToD1 = 0x42324431,                             //B2D1
        BToD2 = 0x42324432,                             //B2D2
        BToD3 = 0x42324433,                             //B2D3
        CalibrationDateTime = 0x63616C74,               //calt
        CharTarget = 0x74617267,                        //targ
        ChromaticAdaptation = 0x63686164,               //chad
        Chromaticity = 0x6368726D,                      //chrm
        ColorantOrder = 0x636C726F,                     //clro
        ColorantTable = 0x636C7274,                     //clrt
        ColorantTableOut = 0x636C6F74,                  //clot
        ColorimetricIntentImageState = 0x63696973,      //ciis
        Copyright = 0x63707274,                         //cprt
        DeviceMfgDesc = 0x646D6E64,                     //dmnd
        DeviceModelDesc = 0x646D6464,                   //dmdd
        DToB0 = 0x44324230,                             //D2B0
        DToB1 = 0x44324231,                             //D2B1
        DToB2 = 0x44324232,                             //D2B2
        DToB3 = 0x44324233,                             //D2B3
        Gamut = 0x67616D74,                             //gamt
        GrayTRC = 0x6B545243,                           //kTRC
        GreenMatrixColumn = 0x6758595A,                 //gXYZ
        GreenTRC = 0x67545243,                          //gTRC
        Luminance = 0x6C756D69,                         //lumi
        Measurement = 0x6D656173,                       //meas
        MediaWhitePoint = 0x77747074,                   //wtpt
        NamedColor2 = 0x6E636C32,                       //ncl2
        OutputResponse = 0x72657370,                    //resp
        PerceptualRenderingIntentGamut = 0x72696730,    //rig0
        Preview0 = 0x70726530,                          //pre0
        Preview1 = 0x70726531,                          //pre1
        Preview2 = 0x70726532,                          //pre2
        ProfileDescription = 0x64657363,                //desc
        ProfileSequenceDesc = 0x70736571,               //pseq
        ProfileSequenceIdentifier = 0x70736964,         //psid
        RedMatrixColumn = 0x7258595A,                   //rXYZ
        RedTRC = 0x72545243,                            //rTRC
        SaturationRenderingIntentGamut = 0x72696732,    //rig2
        Technology = 0x74656368,                        //tech
        ViewingCondDesc = 0x76756564,                   //vued
        ViewingConditions = 0x76696577,                 //view
    }

    /// <summary>
    /// Type Signature
    /// </summary>
    public enum TypeSignature : uint
    {
        Unknown,

        Chromaticity = 0x6368726D,
        ColorantOrder = 0x636c726f,
        ColorantTable = 0x636c7274,
        Curve = 0x63757276,
        Data = 0x64617461,
        DateTime = 0x6474696D,
        Lut16 = 0x6D667432,
        Lut8 = 0x6D667431,
        LutAToB = 0x6D414220,
        LutBToA = 0x6D424120,
        Measurement = 0x6D656173,
        MultiLocalizedUnicode = 0x6D6C7563,
        MultiProcessElements = 0x6D706574,
        NamedColor2 = 0x6E636C32,
        ParametricCurve = 0x70617261,
        ProfileSequenceDesc = 0x70736571,
        ProfileSequenceIdentifier = 0x70736964,
        ResponseCurveSet16 = 0x72637332,
        S15Fixed16Array = 0x73663332,
        Signature = 0x73696720,
        Text = 0x74657874,
        U16Fixed16Array = 0x75663332,
        UInt16Array = 0x75693136,
        UInt32Array = 0x75693332,
        UInt64Array = 0x75693634,
        UInt8Array = 0x75693038,
        ViewingConditions = 0x76696577,
        XYZ = 0x58595A20,

        //V2 Type:
        TextDescription = 0x64657363,
    }

    /// <summary>
    /// Signature Name
    /// </summary>
    public enum SignatureName : uint
    {
        Unknown = 0,

        //colorimetric intent image state
        SceneColorimetryEstimates = 0x73636F65,             //scoe
        SceneAppearanceEstimates = 0x73617065,              //sape
        FocalPlaneColorimetryEstimates = 0x66706365,        //fpce
        ReflectionHardcopyOriginalColorimetry = 0x72686F63, //rhoc
        ReflectionPrintOutputColorimetry = 0x72706F63,      //rpoc

        //Rendering intent gamut
        PerceptualReferenceMediumGamut = 0x70726D67,        //prmg

        //Technology
        FilmScanner = 0x6673636E,                           //fscn
        DigitalCamera = 0x6463616D,                         //dcam
        ReflectiveScanner = 0x7273636E,                     //rscn
        InkJetPrinter = 0x696A6574,                         //ijet
        ThermalWaxPrinter = 0x74776178,                     //twax
        ElectrophotographicPrinter = 0x6570686F,            //epho
        ElectrostaticPrinter = 0x65737461,                  //esta
        DyeSublimationPrinter = 0x64737562,                 //dsub
        PhotographicPaperPrinter = 0x7270686F,              //rpho
        FilmWriter = 0x6670726E,                            //fprn
        VideoMonitor = 0x7669646D,                          //vidm
        VideoCamera = 0x76696463,                           //vidc
        ProjectionTelevision = 0x706A7476,                  //pjtv
        CathodeRayTubeDisplay = 0x43525420,                 //CRT
        PassiveMatrixDisplay = 0x504D4420,                  //PMD 
        ActiveMatrixDisplay = 0x414D4420,                   //AMD 
        PhotoCD = 0x4B504344,                               //KPCD
        PhotographicImageSetter = 0x696D6773,               //imgs
        Gravure = 0x67726176,                               //grav
        OffsetLithography = 0x6F666673,                     //offs
        Silkscreen = 0x73696C6B,                            //silk
        Flexography = 0x666C6578,                           //flex
        MotionPictureFilmScanner = 0x6D706673,              //mpfs
        MotionPictureFilmRecorder = 0x6D706672,             //mpfr
        DigitalMotionPictureCamera = 0x646D7063,            //dmpc
        DigitalCinemaProjector = 0x64636A70,                //dcpj
    }

    /// <summary>
    /// Multi Process ElementSignature
    /// </summary>
    public enum MultiProcessElementSignature : uint
    {
        CurveSet = 0x6D666C74,  //cvst
        Matrix = 0x6D617466,    //matf
        CLUT = 0x636C7574,      //clut
        bACS = 0x62414353,      //bACS
        eACS = 0x65414353,      //eACS
    }

    /// <summary>
    /// Curve Segment Signature
    /// </summary>
    public enum CurveSegmentSignature : uint
    {
        FormulaCurve = 0x70617266,      //parf
        SampledCurve = 0x73616D66,      //samf
    }

    /// <summary>
    /// Colorant Encoding
    /// </summary>
    public enum ColorantEncoding : uint
    {
        Unknown = 0x0000,
        ITU_R_BT_709_2 = 0x0001,
        SMPTE_RP145 = 0x0002,
        EBU_Tech_3213_E = 0x0003,
        P22 = 0x0004,
    }

    /// <summary>
    /// Standard Observer
    /// </summary>
    public enum StandardObserver : uint
    {
        Unkown = 0,
        CIE1931_Observer = 1,
        CIE1964_Observer = 2,
    }

    /// <summary>
    /// Measurement Geometry
    /// </summary>
    public enum MeasurementGeometry : uint
    {
        Unknown = 0,
        MG_0_45_45_0 = 1,
        MG_0d_d0 = 2,
    }

    /// <summary>
    /// Standard Illuminant
    /// </summary>
    public enum StandardIlluminant : uint
    {
        Unknown = 0,
        D50 = 1,
        D65 = 2,
        D93 = 3,
        F2 = 4,
        D55 = 5,
        A = 6,
        Equi_Power_E = 7,
        F8 = 8,
    }

    /// <summary>
    /// Curve Measurement Encodings
    /// </summary>
    public enum CurveMeasurementEncodings : uint
    {
        /// <summary>
        /// ISO 5-3 densitometer response. This is the accepted standard for
        /// reflection densitometers for measuring photographic color prints
        /// </summary>
        StatusA = 0x53746141,   //StaA
        /// <summary>
        /// ISO 5-3 densitometer response which is the accepted standard in
        /// Europe for color reflection densitometers
        /// </summary>
        StatusE = 0x53746145,   //StaE
        /// <summary>
        /// ISO 5-3 densitometer response commonly referred to as narrow band
        /// or interference-type response.
        /// </summary>
        StatusI = 0x53746149,   //StaI
        /// <summary>
        /// ISO 5-3 wide band color reflection densitometer response which is
        /// the accepted standard in the United States for color reflection densitometers
        /// </summary>
        StatusT = 0x53746154,   //StaT
        /// <summary>
        /// ISO 5-3 densitometer response for measuring color negatives
        /// </summary>
        StatusM = 0x5374614D,   //StaM
        /// <summary>
        /// DIN 16536-2 densitometer response, with no polarizing filter
        /// </summary>
        DinE = 0x434E2020,      //DN
        /// <summary>
        /// DIN 16536-2 densitometer response, with polarizing filter
        /// </summary>
        DinE_pol = 0x434E2050,  //DNP
        /// <summary>
        /// DIN 16536-2 narrow band densitometer response, with no polarizing filter
        /// </summary>
        DinI = 0x434E4E20,      //DNN
        /// <summary>
        /// DIN 16536-2 narrow band densitometer response, with polarizing filter
        /// </summary>
        DinI_pol = 0x434E4E50,  //DNNP
    }

    /// <summary>
    /// Profile Conversion Method
    /// </summary>
    public enum ProfileConversionMethod
    {
        Invalid,
        D0,
        D1,
        D2,
        D3,
        A0,
        A1,
        A2,
        ColorTRC,
        GrayTRC,
    }

    /// <summary>
    /// CLUT Data Type
    /// </summary>
    public enum CLUTDataType
    {
        Float,
        UInt8,
        UInt16,
    }
}
