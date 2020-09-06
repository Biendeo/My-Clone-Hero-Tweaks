# Wrappers
These are the wrappers that enable BiendeoCHLib to interact with the game.

## Wrapper Structure
All wrappers must abide by the following structure:

### Declaration
- Wrappers must be declared as `public struct`.
- Wrapper names must be the PascalCase name of the type (often matching it if it is not an obfuscated name), with `Wrapper` afterwards (for example, a wrapper for `Cache` would be `CacheWrapper`).
- Wrappers must include the `Wrapper` attribute, and use the type constructor when possible (for unobfuscated names).
- Wrappers must contain a property with a public getter and private setter for the underlying object it wraps. The object must be its original type if unobfuscated, or `object` otherwise. The name of the underlying type must be the original typename.
- Wrappers must implement a static method called `Wrap` which takes in its appropriate type object and returns a new wrapper:
```cs
public static CacheWrapper Wrap(object cache) => new CacheWrapper {
	Cache = cache
};
```
- Wrappers should also implement `Equals`, `GetHashCode` and `IsNull` as such:
```cs
public override bool Equals(object obj) => Cache.Equals(obj);
public override int GetHashCode() => Cache.GetHashCode();
public bool IsNull() => Cache == null;
```

### Casts
- Any wrappers where the underlying class directly inherits or is directly inherited by another class must contain a cast method for each type.
- The cast must be a method with the naming scheme `CastToX` where `X` is the name of the wrapper class.
- If the underlying class is not a CH class, then it is okay to just perform a straight cast on the wrapped object.
```cs
public BasePlayerWrapper CastToBasePlayer() => BasePlayerWrapper.Wrap(BaseGuitarPlayer);
```

### Constructors
- If any constructors are defined, a region called `Constructors` must be declared.
- Inside this region, all constructors of the class must be exposed as static methods with the name `Construct`. Arguments should directly map to the arguments of the constructor. If the arguments consist of obfuscated typenames, `object` can be used instead. If using `object` causes multiple constructors to have the same prototype, a descriptive name can be used for the method.
```cs
public static CacheWrapper Construct() => new CacheWrapper {
	Cache = defaultConstructor.Invoke(Array.Empty<object>())
};
[WrapperConstructor]
private static readonly ConstructorInfo defaultConstructor;
```

### Fields
- If any fields are defined, a region called `Fields` must be declared.
- Inside this region, all fields of the class must be exposed as public properties with getters and setters if non-constant.
- The type of the field properties must match the original type. If the type uses an obfuscated typename, the wrapper type must be used (with appropriate work in the wrapper property to wrap the returned value in the getter and pass in the wrapped value in the setter). If it is a collection of obfuscated objects, again, additional work must be done to ensure that a collection of wrappers is returned and passed in. If it is another class with a generic of an obfuscated type, panic because I haven't thought that far in advance.
- The name of the field **must** be PascalCase naming, and the underlying FieldInfo must be camelCase with `Field` at the end of its name.
```cs
public int Int1 {
	get => int1Field(Cache);
	set => int1Field(Cache) = value;
}
[WrapperField("\u0313\u0315\u0314\u0317\u0310\u0319\u0310\u030E\u031B\u0317\u0314")]
private static readonly AccessTools.FieldRef<object, int> int1Field;
```

### Properties
- If any properties are defined, a region called `Properties` must be declared.
- Inside this region, all properties of the class must be exposed as public properties with appropriate getters and setters if they exist.
- The same rules for field types and naming applies to properties.
```cs
public int Int3 => (int)int3Property.GetValue(Cache);
[WrapperProperty("\u030F\u0318\u0316\u0311\u031C\u031A\u0318\u031A\u031C\u0319\u0317")]
private static readonly PropertyInfo int3Property;
```

### Methods
- If any methods are defined, a region called `Methods` must be declared.
- Inside this region, all methods of the class must be exposed as public methods with the appropriate arguments.
- The same rules for field types and naming applies to methods.
- Only non-duplicated methods should exist in this region. Harmony can be used to determine which ones actually get called or not.
```cs
public void ScanSongsInternal(bool fullScan) => scanSongsInternalMethod(Cache, fullScan);
[WrapperMethod("\u0310\u031A\u030E\u0318\u0318\u0313\u0316\u0310\u0314\u0318\u0312")]
private static readonly FastInvokeHandler scanSongsInternalMethod;
```

### Enumerations
- If any enums are defined in this class, a region called `Enumerations` must be declared.
- Inside this region, all enums inside this class must be declared, with the same matching names (as PascalCase) and values as the original enum.
- Redefining enums is purely just so usable names can be used. If it's too much work, I guess these can be ignored and just the underlying enum type can be used.
- The original obfuscated name if it exists can just be a comment.
```cs
// \u0315\u0310\u0319\u0315\u0312\u030D\u0312\u0313\u0312\u0314\u0311
public enum CacheState {
	ReadingCache,
	GettingPaths,
	ScanningFolders,
	UpdatingMetadata,
	UpdatingCharts,
	WritingCache,
	WritingBadSongs,
	SortingMetadata,
	OnlineHash
}
```

### Duplicate Methods
- If any duplicate methods are defined in this class, a region called `Duplicate Methods` must be declared.
- The region should have IDE0051 and CS0169 warnings disabled because these fields are intentionally unused.
- Inside this region, all methods that exist purely for obfuscation should only exist as FastInvokeHandlers with the `WrapperMethod` attribute. This is so the wrapper search knows that these methods exist without exposing them.
- The underlying name isn't important but it's best to match the original method name with `Duplicate1` and such at the end.
```cs
#pragma warning disable IDE0051, CS0169 // Remove unused private members

[WrapperMethod("\u0310\u0318\u0318\u030E\u031C\u030D\u030E\u030F\u030E\u030D\u0319")]
private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate1;

[WrapperMethod("\u0315\u0310\u0313\u0318\u030D\u0317\u031A\u030E\u030E\u0314\u0317")]
private static readonly FastInvokeHandler scanSongsInternalMethodDuplicate2;

#pragma warning restore IDE0051, CS0169 // Remove unused private members
```

