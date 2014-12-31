using NServiceBus.Features;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.ObjectBuilder;

namespace NServiceBus.MessagePack
{
    /// <summary>
    /// Uses JSON as the message serialization.
    /// </summary>
    public class SerializationFeature : Feature
    {
        internal SerializationFeature()
        {
            EnableByDefault();
            Prerequisite(this.ShouldSerializationFeatureBeEnabled, "MessagePackSerialization not enable since serialization definition not detected.");
        }

        /// <summary>
        /// See <see cref="Feature.Setup"/>
        /// </summary>
        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Container.ConfigureComponent<MessageMapper>(DependencyLifecycle.SingleInstance);
            var c = context.Container.ConfigureComponent<MessageSerializer>(DependencyLifecycle.SingleInstance);

            context.Settings.ApplyTo<MessageSerializer>((IComponentConfig)c);
        }
    }
}