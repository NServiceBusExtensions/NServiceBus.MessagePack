# <img src="/src/icon.png" height="30px"> NServiceBus.MessagePack

[![Build status](https://ci.appveyor.com/api/projects/status/qxwrielc2o0iyn8a/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/nservicebus-MessagePack)
[![NuGet Status](https://img.shields.io/nuget/v/NServiceBus.MessagePack.svg)](https://www.nuget.org/packages/NServiceBus.MessagePack/)

Add support for [NServiceBus](https://docs.particular.net/nservicebus/) message serialization via [MessagePack](https://github.com/neuecc/MessagePack-CSharp/)

<!--- StartOpenCollectiveBackers -->

[Already a Patron? skip past this section](#endofbacking)


## Community backed

**It is expected that all developers either [become a Patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976) to use NServiceBusExtensions. [Go to licensing FAQ](https://github.com/NServiceBusExtensions/Home/#licensingpatron-faq)**


### Sponsors

Support this project by [becoming a Sponsor](https://opencollective.com/nservicebusextensions/contribute/sponsor-6972). The company avatar will show up here with a website link. The avatar will also be added to all GitHub repositories under the [NServiceBusExtensions organization](https://github.com/NServiceBusExtensions).


### Patrons

Thanks to all the backing developers. Support this project by [becoming a patron](https://opencollective.com/nservicebusextensions/contribute/patron-6976).

<img src="https://opencollective.com/nservicebusextensions/tiers/patron.svg?width=890&avatarHeight=60&button=false">

<a href="#" id="endofbacking"></a>

<!--- EndOpenCollectiveBackers -->


## Usage

snippet: MessagePackSerialization

This serializer does not support [messages defined as interfaces](https://docs.particular.net/nservicebus/messaging/messages-as-interfaces). If an explicit interface is sent, an exception will be thrown with the following message:

```
Interface based message are not supported.
Create a class that implements the desired interface
```

Instead, use a public class with the same contract as the interface. The class can optionally implement any required interfaces.


### Resolver

Customizes the instance of `IFormatterResolver` used for serialization.

snippet: MessagePackResolver


### Custom content key

When using [additional deserializers](https://docs.particular.net/nservicebus/serialization/#specifying-additional-deserializers) or transitioning between different versions of the same serializer it can be helpful to take explicit control over the content type a serializer passes to NServiceBus (to be used for the [ContentType header](https://docs.particular.net/nservicebus/messaging/headers#serialization-headers-nservicebus-contenttype)).

snippet: MessagePackContentTypeKey


## Icon

[Backpack](https://thenounproject.com/term/backpack/75402/) designed by [Nathan Driskell](https://thenounproject.com/driskell/) from [The Noun Project](https://thenounproject.com).