![Icon](https://raw.githubusercontent.com/SimonCropp/NServiceBus.MessagePack/master/Icon/package_icon.png)

NServiceBus.MessagePack
===========================

Add support for [NServiceBus](http://particular.net/NServiceBus) message serialization via [MessagePack](https://github.com/msgpack/msgpack-cli)

## Nuget

### http://nuget.org/packages/NServiceBus.MessagePack/

    PM> Install-Package NServiceBus.MessagePack

## Usage

```
var busConfig = new BusConfiguration();
busConfig.UseSerialization<MessagePackSerializer>();
```

## Icon

<a href="http://thenounproject.com/term/backpack/75402/" target="_blank">Backpack</a> designed by <a href="http://thenounproject.com/driskell/" target="_blank">Nathan Driskell</a> from The Noun Project
