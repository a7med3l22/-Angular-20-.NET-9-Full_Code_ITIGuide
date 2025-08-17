using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Specification
{
	public interface ISpecifications<T> where T : BaseClass
	{
		Expression<Func<T, bool>>? Criteria { get; }
		List<Expression<Func<T, object>>> Includes { get; set; }
		public int Take { get; set; } // for pagination
		public int Skip { get; set; } // for pagination
		public bool IsPagingEnabled { get; set; } // for pagination
		public Expression<Func<T, object>>? OrderBy { get; set; }
		public Expression<Func<T, object>>? OrderByDesc { get; set; }
	}
}
