using System;
namespace Cards {
	public abstract class AbstractCardInfo{
		public String description;
		public AbstractCardInfo( String description) {
			this.description = description;
		}  

		public void SetValues(String description){
			this.description = description;
		}
	}
}
