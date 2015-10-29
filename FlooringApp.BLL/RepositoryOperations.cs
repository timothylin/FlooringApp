using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Data.Products;
using FlooringApp.Data.States;
using FlooringApp.Models;
using FlooringApp.Models.Interfaces;

namespace FlooringApp.BLL
{
    public class RepositoryOperations
    {

        private IStateRepository _statesRepository;
        private IProductsRepository _productsRepository;
        private Response _response;

        public RepositoryOperations()
        {
            _statesRepository = StatesRepositoryFactory.CreateStatesRepository();
            _productsRepository = ProductsRepositoryFactory.CreateProductsRepository();
            _response = new Response();
        }

        public Response CheckIfStateExists(State stateQuery)
        {
            Logger.Info("Check if state exists called", "RepoOps - CheckIfStateExists");

            var state = _statesRepository.GetState(stateQuery);

            if (state == null)
            {
                Logger.Warning("State does not exist", "RepoOps - CheckIfStateExists");
                _response.Success = false;
                return _response;
            }
            else
            {
                _response.Success = true;
                _response.State = state;
                return _response;
            }
        }


        public Response CheckIfProductExists(Product productQuery)
        {
            Logger.Info("Check if product exists called", "RepoOps - CheckIfProductExists");

            var product = _productsRepository.GetProduct(productQuery);

            if (product == null)
            {
                Logger.Warning("Product does not exist", "RepoOps - CheckIfProductExists");
                _response.Success = false;
                return _response;
            }
            else
            {
                _response.Success = true;
                _response.Product = product;
                return _response;
            }
        }
    }
}
