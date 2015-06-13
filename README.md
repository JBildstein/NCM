# NCM
A .Net color conversion/managment library

## Status

- Color conversion is fully working
- ICC reading works but needs testing with more profiles
- ICC conversion is a WIP

## Plans

- Complete ICC conversion
- Add complete documentation
- Implement ICC writer
- Make stuff faster

## Basic Usage

    var col1 = new ColorRGB(0.35, 0.17, 0.63, new Colorspace_AdobeRGB());
    var col2 = new ColorXYZ(new WhitepointD50());

    using (ColorConverter conv = new ColorConverter(col1, col2))
    {
        conv.Convert();
        
        Console.WriteLine(col2.X);
        Console.WriteLine(col2.Y);
        Console.WriteLine(col2.Z);
    }

If you want to change the input color, it's best to use it like this:

    col1[0] = 0.53;
    col1[1] = 0.72;
    col1[2] = 0.24;
Then call conv.Convert(); again to convert the color.

In general, keep in mind that creating a ColorConverter is expensive while the Convert method is cheap.