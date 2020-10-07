## Stylized Toon Lighting Effect ##
Written in C# with Microsoft XNA/Monogame. Originally written in March 2017. 
A stylized, dynamic lightning effect created programmatically so that the effect doesn't have to be manually animated.

Creates a bolt of lightning between two points

    ToonLightning(int segments, int width, Vector2 startPoint, Vector2 endPoint, Vector2 lengthRange)

With LengthRange describing how much the segments of the bolt vary along the path, with a small number giving very little variation and therefore a quite smooth looking bolt and larger number resulting in much more pronounced spikiness. 

![Animation of the lightning](https://github.com/GryffDavid/READMEImages/blob/master/StylizedLightning/StylizedLightningAnimation.gif)

[YouTube Video](https://youtu.be/Vw5ROUqf0Dk)
