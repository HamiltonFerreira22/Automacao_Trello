using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondTrello
{
    class Trello
    {

        // Elementos Login       
        public string botao_login = "//a[@href='/login']/span";
        public string campo_email = "//input[@inputmode='email2']";
        public string botao_login_Atla = "//input[@value='Fazer login com a Atlassian']";
        public string campo_senha = "//input[@id='password']";        

        // Elementos Logout
        public string menu_mebros = "//button[@aria-label='Abrir Menu de Membros']";
        public string fazer_logout = "//button[@data-test-id='header-member-menu-logout']";
        public string botao_sair = "//button[@id='logout-submit']";

        // botoes para o fluxo de criar quadro
        public string criar_novo_quadro = "//span[contains(text(),'Criar novo quadro')]";
        public string nome_quadro = " //input[@aria-label='Adicionar título do quadro']";
        public string bot_criar_quadro = "//button[@data-test-id='create-board-submit-button']";
        public string sair_quadro = "//div[@class='_2ft40Nx3NZII2i']";

        // botoes para o fluxo de excluir quadro
        public string quadro_botao_mostrar_menu = "//span[contains(text(),'Mostrar Menu')]";
        public string quadro_menu_botao_mais = "//a[contains(@class,'open-more')]/span";
        public string quadro_menu_botao_fechar_quadro = "//a[contains(text(), 'Fechar Quadro')]";
        public string quadro_menu_botao_fechar = "//input[@value='Fechar']";
        public string quadro_menu_botao_excluir = "//button[contains(text(), 'Excluir o quadro permanentemente')]";
        public string quadro_menu_confirma_excluir = "//button[@data-test-id='close-board-delete-board-confirm-button']";




        public void paginaInicial()
        {
            Global.Elements_Extensions.Navigate(Global.driver, "https://trello.com/");
        }

        public void login(string user, string pass)
        {           
            Global.Elements_Extensions.Click(Global.driver, By.XPath(botao_login));
            Global.Elements_Extensions.SendKeys(Global.driver, By.Id("user"), "");
            Global.Elements_Extensions.Click(Global.driver, By.XPath(botao_login_Atla));
            Global.Elements_Extensions.SendKeys(Global.driver, By.Name("password"), "");
            Global.Elements_Extensions.Click(Global.driver, By.Id("login-submit"));
            string textoMsg = "Login realizado";
            if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//span[contains(text(),'Criar novo quadro')]"), 10))
            {
                textoMsg = "Botão criar quadro não existe";
            }
            Assert.AreEqual("Login realizado", textoMsg);
        }

        public void CriarQuadro(string nomeQuadro)
        {
            if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[contains(text(),'" + nomeQuadro + "')]"), 10))
            {
                Global.Elements_Extensions.Click(Global.driver, By.XPath(criar_novo_quadro));
                Global.Elements_Extensions.SendKeys(Global.driver, By.XPath(nome_quadro), nomeQuadro);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(bot_criar_quadro));
                string textoMsg = "Quadro criado com sucesso!";
                if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[contains(text(),'" + nomeQuadro + "')]"), 10))
                {
                    textoMsg = "Botão criar quadro não existe";
                }
                Assert.AreEqual("Quadro criado com sucesso!", textoMsg);
            }
        }

        public void EntrarQuadro(string nomeQuadro)
        {
            if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[contains(text(),'" + nomeQuadro + "')]"), 10))
            {
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[contains(text(), '" + nomeQuadro + "')]"), 500);
            }
        }      

        //Elementos Criar Lista 

        public void CriarLista(string nomeLista)
        {
            if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//h2[contains(text(),'" + nomeLista + "')]"), 3))
            {
                if (!Global.Elements_Extensions.IsVisible(Global.driver, By.XPath("//*[@id='board']/div/form/input")))
                {
                    if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//span[contains(text(),'Adicionar outra lista')]"), 1))
                    {
                        Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(),'Adicionar outra lista')]"), 500);
                    }
                    else
                    {
                        Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(),'Adicionar uma lista')]"));
                    }
                }
                Global.Elements_Extensions.SendKeys(Global.driver, By.XPath("//*[@id='board']/div/form/input"), nomeLista);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//*[@id='board']/div/form/div/input"));
                string textoMsg = "Lista Criada com sucesso!";
                if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//h2[contains(text(),'" + nomeLista + "')]"), 500))
                {
                    textoMsg = "Lista não existe";
                }
                Assert.AreEqual("Lista Criada com sucesso!", textoMsg);
            }
        }
        public void ArquivarLista(string nomeLista)
        {
            if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//h2[contains(text(),'" + nomeLista + "')]"), 10))
            {
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[h2[contains(text(),'" + nomeLista + "')]]/div/a[@aria-label='Ações da lista']"), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//a[contains(text(), 'Arquivar Esta Lista')]"), 500);
            }
        }
        public void CriarCartao(string nomeLista, string nomeCartao)
        {
            if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/div/span[contains(text(),'" + nomeCartao + "')]"), 3))
            {
                if (Global.Elements_Extensions.IsVisible(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/span[contains(text(),'Adicionar um cartão')]")))
                {
                    Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/span[contains(text(),'Adicionar um cartão')]"), 500);
                }
                Global.Elements_Extensions.SendKeys(Global.driver, By.XPath("//textarea[contains(@placeholder, 'Insira um título para este cartão')]"), nomeCartao);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//input[@value='Adicionar Cartão']"), 500);
            }
        }

        public void ArquivarCartao(string nomeLista, string nomeCartao)
        {
            if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/div/span[contains(text(),'" + nomeCartao + "')]"), 10))
            {
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/div/span[contains(text(),'" + nomeCartao + "')]"), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(), 'Arquivar')]"), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(), 'Excluir')]"), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath("//input[@value='Excluir']"), 500);
            }
        }
        public void Etiqueta(string nomeLista, string nomeCartao, string corEtiqueta)
        {
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/div/span[contains(text(),'" + nomeCartao + "')]"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(), 'Etiquetas')]"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//*[@data-color='" + corEtiqueta + "']"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//a[@class='pop-over-header-close-btn icon-sm icon-close']"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath(" //a[@class='icon-md icon-close dialog-close-button js-close-window']"), 500);
        }
        public string EtiquetaRandomico(string nomeLista, string nomeCartao)
        {

            Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[div/h2[contains(text(),'" + nomeLista + "')]]/div/a/div/span[contains(text(),'" + nomeCartao + "')]"), 3);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(text(), 'Etiquetas')]"), 500);
            //recuperar qtas ocorrencias uma mascara corresponde
            int cores = Global.Elements_Extensions.GetElementsCount(Global.driver, By.XPath("//div[h4[contains(text(),'Etiquetas')]]/ul/li"));
            //utilizando metodo random
            Random rand = new Random();
            int index = rand.Next(0, cores);

            string[] tabelaCores = { "green", "yellow", "orange", "red", "purple", "blue" };

            string nomeCor = tabelaCores[index];
            
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//span[contains(@data-color,'" + nomeCor + "')]"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//a[@class='pop-over-header-close-btn icon-sm icon-close']"), 500);
            Global.Elements_Extensions.Click(Global.driver, By.XPath("//a[@class='icon-md icon-close dialog-close-button js-close-window']"), 500);
            return nomeCor;
        }
        public void SairQuadro()
        {
            Global.Elements_Extensions.Click(Global.driver, By.XPath(sair_quadro), 500);
        }
        public void ExcluirQuadro(string nomeQuadro, string posicao = "interno")
        {
            bool apaga = true;
            if (posicao == "externo")
            {
                if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[contains(text(),'" + nomeQuadro + "')]"), 3))
                {
                    Global.Elements_Extensions.Click(Global.driver, By.XPath("//div[contains(text(), '" + nomeQuadro + "')]"), 500);
                }
                else
                {
                    apaga = false;
                }
            }
            if (apaga)
            {
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_botao_mostrar_menu), 1);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_menu_botao_mais), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_menu_botao_fechar_quadro), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_menu_botao_fechar), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_menu_botao_excluir), 500);
                Global.Elements_Extensions.Click(Global.driver, By.XPath(quadro_menu_confirma_excluir), 500);
                string textoMsg = "Quadro Excluido com sucesso";
                if (Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//div[contains(text(),'" + nomeQuadro + "')]"), 10))
                {
                    textoMsg = "Quadro Excluir não existe";
                }
                Assert.AreEqual("Quadro Excluido com sucesso", textoMsg);
            }
        }

        public void logout()
        {
            Global.Elements_Extensions.Click(Global.driver, By.XPath(menu_mebros));
            Global.Elements_Extensions.Click(Global.driver, By.XPath(fazer_logout));
            Global.Elements_Extensions.Click(Global.driver, By.XPath(botao_sair));
            string textoMsg = "Logout realizado";
            if (!Global.Elements_Extensions.WaitExists(Global.driver, By.XPath("//p[contains(text(),'Você já saiu')]"), 10))
            {
                textoMsg = "Mensagem de despedida não existe";
            }
            Assert.AreEqual("Logout realizado", textoMsg);
        }
    }
}
