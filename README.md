# PassWarden

**PassWarden** is a powerful and versatile library designed for working with passwords, aimed at developers looking to enhance the security of their applications and services. 
It provides a comprehensive set of tools for password *generation, analysis, validation, and hashing*, enabling efficient management of user account security. 
The library includes several key classes, each serving a specific purpose in the realm of password security. 

**PassWarden** equips developers with intuitive methods for creating and verifying passwords, making it an ideal solution for improving application security. 
Its usage not only helps generate high-quality passwords but also allows for comprehensive analysis of passwords for vulnerabilities, as well as integrating robust hashing and validation mechanisms into any project.

## Documentation

Functionality for password *analysis*:
1. `IsPwned()` allows you to check the password for presence in the open source, using the API *api.pwnedpasswords.com*.
2. `GetEntropy()` calculates the entropy of the specified password, which represents its randomness and unpredictability.
3. `GetStrength()` evaluates the strength of the specified password based on its complexity, length, and other factors.
4. `GetSymbolFrequency()` analyzes the frequency of each character in the specified password.
5. `DetectPatterns()` finds patterns in the specified password, such as repeating characters, consecutive letters or numbers.
6. `GetPasswordSimilarity()` calculates the similarity between two passwords by comparing their characters at the same positions.

Functionality for password *generation*:
1. `GenerateRandom()` generates a random strong password consisting of a random set of letters, numbers and symbols.
2. `Generate()` creates a password based on the provided rules.
3. `GenerateMnemonic()` generates a mnemonic-based password, which combines a noun, adjective, verb, special character, and digit.
4. `GenerateFromPhrase()` generates a password derived from the specified phrase, applying character substitutions.

Functionality for password *validation*:
1. `ValidatePassword()` validates a password against a regular expression pattern.
2. `ValidateByStopList()` validates a password by ensuring it does not contain any items from a stop list.
3. `ValidatePassword()` validates a password based on a set of rules.

In addition, there is functionality for generating a password hash based on the *bcrypt* algorithm. The functionality is in the `HashProvider` class. 
Among other things, it is possible to calculate the approximate time to crack the specified password using the specified algorithm. The functionality for this is in the `PassWarden` class.

## Usage

To use the functionality, you need to initialize the PassWarden object:
```csharp
var passWarden = new PassWarden();
```

This is the main object that provides access to password analysis, generation and validation through class properties:
```csharp
var analyzer = passWarden.Analyzer;
var generator = passWarden.Generator;
var validator = passWarden.Validator;
var hashProvider = passWarden.HashProvider;
```

The main class `PassWarden` implements a method `CalculateBruteForceTime` for calculating the approximate time it takes to crack a password:
```csharp
var result = passWarden.CalculateBruteForceTime("qwerty1234"); // by default returns time in days
```

The `Analyzer` property contains all the methods needed to analyze passwords, so use this property to access this functionality:
```csharp
string password = "_QweRtY_ZXc60w21lkhh!";
var result = passWarden.Analyzer.GetEntropy(password);
Console.WriteLine(result > 100);
```

If you want to generate a password, you can use the Generator property to access the corresponding functionality:
```csharp
var rules = new PasswordGenerationRules
{
    NumberOfDigits = 3,
    NumberOfLowercase = 3,
    NumberOfSpecialCharacters = 1,
};
var password = passWarden.Generator.Generate(rules);
```

## Installation

To install the package, use the following command:
```bash
dotnet add package PassWarden
```

or via NuGet Package Manager:
```csharp
Install-Package PassWarden
```
