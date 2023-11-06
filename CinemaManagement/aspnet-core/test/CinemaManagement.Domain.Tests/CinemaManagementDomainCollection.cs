using CinemaManagement.MongoDB;
using Xunit;

namespace CinemaManagement;

[CollectionDefinition(CinemaManagementTestConsts.CollectionDefinitionName)]
public class CinemaManagementDomainCollection : CinemaManagementMongoDbCollectionFixtureBase
{

}
