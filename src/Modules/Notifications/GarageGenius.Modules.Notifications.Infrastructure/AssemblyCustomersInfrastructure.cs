using System.Reflection;

namespace GarageGenius.Modules.Notifications.Infrastructure;
internal static class AssemblyNotificationsInfrastructure
{
	public static Assembly AssemblyReference => typeof(AssemblyNotificationsInfrastructure).Assembly;
}
