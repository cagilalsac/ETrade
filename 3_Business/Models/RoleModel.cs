#nullable disable

using Core.Records.Bases;
using System.ComponentModel;

namespace Business.Models
{
	public class RoleModel : Record
	{
		#region Entity'den Kopyalanan Özellikler
		[DisplayName("Role")]
		public string Name{ get; set; }
		#endregion
	}
}
