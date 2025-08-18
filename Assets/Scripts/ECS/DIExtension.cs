using Scellecs.Morpeh;
using VContainer;

namespace ECS
{
    public static class DIExtension
    {
        public static RegistrationBuilder RegisterSystem<T>(this IContainerBuilder builder, Lifetime lifetime)
            where T : ISystem =>
            builder.Register<ISystem, T>(lifetime);

        public static RegistrationBuilder RegisterCleanupSystem<T>(this IContainerBuilder builder, Lifetime lifetime)
            where T : ICleanupSystem =>
            builder.Register<ICleanupSystem, T>(lifetime);
    }
}