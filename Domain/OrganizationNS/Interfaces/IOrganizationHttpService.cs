namespace Domain.OrganizationNS.Interfaces
{
    public interface IOrganizationHttpService
    {
        public Task<IEnumerable<Organization>> GetAll();
        public Task<IEnumerable<Organization>> GetOrganizationsByUId(IEnumerable<Guid> uids);
    }
}
