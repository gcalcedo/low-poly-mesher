# Low Poly Mesher (LPM)

LPM, integrated as a Unity Engine plugin, allows developers to define templates that generate low-poly meshes at runtime. This lifts developers from spending time in repetitive tasks such as creating variations of a 3D model. Once a template is defined, LPM automatically creates variations from it.

## How it works ⚙️
Templates can be built from scratch or on top of simpler ones. This package provides a set of *primitive* templates under ```Assets/Scripts/Mesh Templates/Primitives``` and some sample custom ones built with primitives under ```Assets/Scripts/Mesh Templates/Nature```.

While templates provide the basic shape of a 3D model, automatic variations are created through *modifications*. Basic modifications are located under ```Assets/Scripts/Mesh Modifications```. Similarly to templates, custom modifications can be defined on their own or based on other ones.

Additionally, modifications can be feed into the ```.Anim()``` method to animate the generated 3D models through an interpolation between its original and modified states in a loop.

This snippet shows a basic custom template.
```cs
[System.Serializable]
[TemplatePath("Test", typeof(TestObject))]
public class TestObject : MeshTemplate
{
    public override IEnumerable<MeshPackage> Generate()
    {
        return MeshPackage.Build(
            new Box(6, 5, 6)
                .Mod(NoisePosition.X(0.2f, NoiseMode.DYNAMIC))
                .Anim(NoisePosition.Y(1f, NoiseMode.DYNAMIC), 1),
            new Pyramid(6, 5, 6)
                .Mod(Traslation.Y(3))
                .Mod(Rotation.Z(15))
                .Color("#A7F070")
            )
        );
    }
}
```

## Some examples 🌲
The following meshes are generated in their entirety by LPM.


![thumb_0148](https://user-images.githubusercontent.com/49457798/226219026-f05f98c6-b01b-4183-bd7d-e9e581e6a176.jpg)
![thumb_0149](https://user-images.githubusercontent.com/49457798/226219029-39b04e32-64f3-408e-9e02-e0cb3e0b6db8.jpg)
![thumb_0150](https://user-images.githubusercontent.com/49457798/226219031-6bd1703a-8970-48b6-8d94-8cda7cce5464.jpg)
