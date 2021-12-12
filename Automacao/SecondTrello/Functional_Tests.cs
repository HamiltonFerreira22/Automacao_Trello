using NUnit.Framework;
using System;

namespace SecondTrello
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            // Instância dos objetos
            Global.Elements_Extensions = new Elements_Extensions();
            Global.trello = new Trello();           

            // Instância do driver
            Global.driver = Global.Elements_Extensions.BrowserConfig();

            // Etapa comum aos testes
            Global.trello.paginaInicial();
            Global.trello.login(user, pass);
        }
        [Test]
        public void EmBranco()
        {

        }

        [Test]
        public void CriarQuadro()
        {
            Global.trello.CriarQuadro("Título Teste1");

        }

        [Test]
        public void BatchQuadro()
        {
            for (int i = 1; i <= 3; i++)
            {
                Global.trello.CriarQuadro("Título Teste" + i.ToString());
                Global.trello.SairQuadro();
            }
        }

        [Test]
        public void CriarLista()
        {
            Global.trello.EntrarQuadro("Título Teste1");
            for (int i = 1; i <= 3; i++)
            {
                Global.trello.CriarLista("Lista Teste" + i.ToString());
            }

        }

        [Test]
        public void ArquivarLista()
        {
            Global.trello.EntrarQuadro("Título Teste1");
            Global.trello.ArquivarLista("Lista Teste1");
            
        }

        [Test]
        public void CriarCartao()
        {
            Global.trello.EntrarQuadro("Título Teste1");
            //Global.trello.CriarLista("Lista Teste1");
            Global.trello.CriarCartao("Lista Teste1", "Cartao Teste1");
           
        }

        [Test]
        public void ArquivarCartao()
        {
            Global.trello.EntrarQuadro("Título Teste1");            
            Global.trello.ArquivarCartao("Lista Teste1", "Cartao Teste1");
           
        }

        [Test]
        public void Etiqueta()
        {
            Global.trello.EntrarQuadro("Título Teste1");
            Global.trello.CriarLista("Lista Teste1");
            Global.trello.CriarCartao("Lista Teste1", "Cartao Teste1");
            Global.trello.Etiqueta("Lista Teste1", "Cartao Teste1", "red");
        }

        [Test]
        public void EtiquetaRandomico()
        {
            Global.trello.EntrarQuadro("Título Teste1");
            Global.trello.CriarLista("Lista Teste1");
            Global.trello.CriarCartao("Lista Teste1", "Cartao Teste1");
            Global.trello.EtiquetaRandomico("Lista Teste1", "Cartao Teste1");
            Global.trello.EtiquetaRandomico("Lista Teste1", "Cartao Teste1");
            Global.trello.ArquivarCartao("Lista Teste1", "Cartao Teste1");
            Global.trello.ArquivarLista("Lista Teste1");
        }

        [Test]
        public void FluxoSimples()
        {
            Global.trello.CriarQuadro("Treinamento Trello2");

            int totalListas = 3;
            int totalCartoes = 3;
            //array multidimensional
            string[,] tabela_cores = new string[totalListas, totalCartoes];
            for (int l1 = 1; l1 <= totalListas; l1++)
            {
                Global.trello.CriarLista("Lista " + l1.ToString());

                for (int c1 = 1; c1 <= totalCartoes; c1++)
                {
                    Global.trello.CriarCartao("Lista " + l1.ToString(), "Cartão " + c1.ToString());
                    tabela_cores[l1 - 1, c1 - 1] = Global.trello.EtiquetaRandomico("Lista " + l1.ToString(), "Cartão " + c1.ToString());
                }
            }

            Global.Elements_Extensions.Wait(4000);
            for (int l2 = totalListas; l2 >= 1; l2--)
            {
                for (int c2 = totalCartoes; c2 >= 1; c2--)
                {
                    
                    Global.trello.Etiqueta("Lista " + l2.ToString(), "Cartão " + c2.ToString(), tabela_cores[l2 - 1, c2 - 1]);
                    Global.trello.ArquivarCartao("Lista " + l2.ToString(), "Cartão " + c2.ToString());
                }
                Global.trello.ArquivarLista("Lista " + l2.ToString());
            }
           
            Global.trello.ExcluirQuadro("Treinamento Trello2");
            
        }
    

        [Test]
        public void ExcluirQuadro()
        {
            Global.trello.ExcluirQuadro("Título Teste1", "externo");
        }

        [Test]
        public void SairQuadro()
        {
            Global.trello.SairQuadro();

        }

        [Test]
        public void EntrarQuadro()
        {
            Global.trello.EntrarQuadro("teste");
        }

        [TearDown]
            public void TeardownTest()
            {
                // Finalização do browser
                try
                {
                    Global.trello.logout();
                    Global.driver.Quit();
                }
                catch (Exception)
                {

                }

            }

    }
       
       
}   
       
    