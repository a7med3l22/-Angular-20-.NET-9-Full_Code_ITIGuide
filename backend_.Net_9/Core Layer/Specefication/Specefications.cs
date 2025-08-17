using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Specefication;

namespace Core_Layer.Specification
{
	public class Specifications<T>: ISpecifications<T> where T : BaseClass
	{
		//where(p=>p.id==5&&p.name=="ahmed") //this is a filter condition
		//include(p => p.Brand).include(p => p.Category) //this is a include condition 

		//p=>p.id==5&&p.name=="ahmed" //T,Bool
		public Expression<Func<T, bool>>? Criteria { get;  set; } //T,Bool
		//p => p.Brand , p => p.Category  //List<T,object>
		public List<Expression<Func<T, object>>> Includes { get; set; } = new(); //T,List<T>
		public int Take { get; set; } // for pagination
		public int Skip { get; set; } // for pagination
		public bool IsPagingEnabled { get; set; } // for pagination
		public Expression<Func<T, object>>? OrderBy { get; set; }
		public Expression<Func<T, object>>? OrderByDesc { get; set; }
		
	}
}
