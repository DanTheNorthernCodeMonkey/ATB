using System;
using System.Threading.Tasks;

namespace ATB.BackOffice.Domain
{
	public interface IBeanAdvertManager
	{
		Task<DateStatus> GetDateStatus(DateTime date);
	}
}