using System.Collections.Generic;
using System.Linq;

namespace ATB.Infrastructure
{
	public class ExecutionResult<T>
	{
		public ExecutionStatus Status { get; set; }

		public T Data => DataSet.FirstOrDefault();

		public IEnumerable<T> DataSet { get; set; }
	}
}
