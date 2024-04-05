using CqrsMediatrExample.Queries;
using MediatR;

namespace CqrsMediatrExample.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>    
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetProductHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
            await _fakeDataStore.GetProductsAsync();
    }
}
