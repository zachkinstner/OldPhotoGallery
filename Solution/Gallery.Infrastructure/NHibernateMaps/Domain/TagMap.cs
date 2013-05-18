using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Gallery.Domain;

namespace Gallery.Infrastructure.NHibernateMaps.Domain {
	
	/*================================================================================================*/
	public class TagMap : IAutoMappingOverride<Tag> {

		public void Override(AutoMapping<Tag> pMapping) {
			pMapping.Map(x => x.Name).Length(24);
			pMapping.HasMany(x => x.PhotoTags);
		}

	}
}
