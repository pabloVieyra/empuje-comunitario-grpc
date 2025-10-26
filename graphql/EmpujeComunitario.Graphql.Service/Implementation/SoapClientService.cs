using DocumentFormat.OpenXml.Office2010.Excel;
using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.Service.Interface;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Implementation
{
    public class SoapClientService : ISoapClientService
    {
        private readonly ApplicationClient _client;
        public SoapClientService() 
        {
            _client = new ApplicationClient();
        }

        public async Task<BaseObjectResponse<list_associationsResponse1>> GetAllOrganization(List<string> id)
        {
            var response = new BaseObjectResponse<list_associationsResponse1>();
            try
            {
                using (new OperationContextScope(_client.InnerChannel))
                {
                    // Agregamos el header <auth:Auth>
                    var authHeader = MessageHeader.CreateHeader(
                        "Auth",            // nombre del nodo
                        "auth.headers",    // namespace xmlns:auth
                        new AuthHeaderData
                        {
                            Grupo = "GrupoA-TM",
                            Clave = "clave-tm-a"
                        }
                    );

                    OperationContext.Current.OutgoingMessageHeaders.Add(authHeader);

                    // Armamos la request
                    var request = new list_associations1
                    {
                        list_associations = new list_associations
                        {
                            org_ids = id.ToArray()
                        }
                    };

                    // Llamada SOAP
                    var result = await _client.list_associationsAsync(request);

                    return response.OkWithData(result);
                }

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }


        public async Task<BaseObjectResponse<list_presidentsResponse1>> GetAllPresident(List<string> id)
        {
            var response = new BaseObjectResponse<list_presidentsResponse1>();
            try
            {
                using (new OperationContextScope(_client.InnerChannel))
                {
                    // Agregamos el header <auth:Auth>
                    var authHeader = MessageHeader.CreateHeader(
                        "Auth",            // nombre del nodo
                        "auth.headers",    // namespace xmlns:auth
                        new AuthHeaderData
                        {
                            Grupo = "GrupoA-TM",
                            Clave = "clave-tm-a"
                        }
                    );
                    OperationContext.Current.OutgoingMessageHeaders.Add(authHeader);

                    var request = new list_presidents1(
                        new list_presidents
                        {
                            org_ids = id.ToArray()
                        });
                    var result = await _client.list_presidentsAsync(request);
                    return response.OkWithData(result);
                
                }

            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }
    }
}
