using System.Collections.Generic;
using System.Reflection;
using AutoFixture;
using AutoMapper;
using AutoMapper.Internal;
using Dfe.Academies.External.Web.AutoMapper;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models.EmailTemplates;
using FluentAssertions;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Automapper
{
	public class AutoMapperSetupTests
	{
		private Fixture _fixture = new Fixture();
		private IMapper _mapper;
		public AutoMapperSetupTests()
		{
			_fixture.Customize(new AutoPopulatedMoqPropertiesCustomization());			

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			});
			
			mapperConfig.AssertConfigurationIsValid();
			_mapper = new Mapper(mapperConfig);
		}

		[Test]
		public void Map_WhenGivenEmailVariablesDto_ReturnsDictionary()
		{
			var emailTemplate = _fixture.Create<EmailVariablesDto>();
			var result = _mapper.Map<IDictionary<string, dynamic>>(emailTemplate);
			foreach (var propertyInfo in emailTemplate.GetType().GetProperties())
			{
				var value = propertyInfo.GetValue(emailTemplate, null);
				result.Values.Should().Contain(value);
			}
		}
	}
}
