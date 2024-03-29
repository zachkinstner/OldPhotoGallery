﻿using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Gallery.Domain;

namespace Gallery.Infrastructure.NHibernateMaps.Domain {
	
	/*================================================================================================*/
	public class PhotoTagMap : IAutoMappingOverride<PhotoTag> {

		public void Override(AutoMapping<PhotoTag> pMapping) {
			pMapping.References(x => x.Photo).UniqueKey("pt");
			pMapping.References(x => x.Tag).UniqueKey("pt");
		}

	}
}
