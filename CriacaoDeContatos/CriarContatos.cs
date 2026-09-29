using System;
using System.IO;
using System.Collections.Generic;

namespace CriacaoDeContatos
{
    internal class CriarContatos
    {

        public static void progContato()
        {
            string caminho = "contatos.txt";
            bool ativo = true;

            try
            {
                while (ativo)
                {
                    Console.WriteLine("\n=== Gerenciador de Contatos ===\n" +
                        "escolha uma opção: \n" +
                        "1: Adicionar novo contato \n" +
                        "2: Listar contatos cadastrados \n" +
                        "3: Sair");
                    String opcao = Console.ReadLine();

                    if (opcao == "1")
                    {
                        Console.WriteLine("\n--------------------------------------");
                        CriarContatos.cadastrar(caminho: caminho);
                        Console.WriteLine("--------------------------------------");
                    }
                    else if (opcao == "2")
                    {
                        List<Contato> contatos = listar(caminho: caminho);
                        ContatoFormatter formato;

                        if (contatos.Count > 0)
                        {
                            Console.WriteLine("\nEscolha a forma de exibição:\n" +
                                "1: Markdown\n" +
                                "2: Tabela \n" +
                                "3: Texto puro");
                            String formaExbicao = Console.ReadLine();

                            switch (formaExbicao)
                            {
                                case "1":
                                    formato = new MarkdownFormatter();
                                    break;
                                case "2":
                                    formato = new TabelaFormatter();
                                    break;
                                case "3":
                                    formato = new RawTextFormatter();
                                    break;
                                default:
                                    Console.WriteLine("Formato inválido.");
                                    continue;
                            }

                            formato.ExibirContatos(contatos);
                        }

                    }
                    else if (opcao == "3")
                    {
                        Console.WriteLine("\nSaindo...");
                        ativo = false;
                    }
                    else { Console.WriteLine("valor invalido"); }

                }
            }catch (Exception e) { Console.WriteLine($"Houve um erro inesperado: {e.Message}"); }
        }

        
        //-----------------------------------------------
        //fazer cadastro
        protected static void cadastrar(String caminho)
        {
            Console.WriteLine("Nome: ");
            String nome = Console.ReadLine();

            Console.WriteLine("Telefone: ");
            String sTelefone = Console.ReadLine();

            Console.WriteLine("Email: ");
            String email = Console.ReadLine();

            if (!string.IsNullOrEmpty(nome)
                 && !string.IsNullOrEmpty(sTelefone)
                 && !string.IsNullOrEmpty(email))
            {
                using (StreamWriter sw = new StreamWriter(caminho, true))
                {
                    sw.WriteLine($"{nome},{sTelefone},{email}");
                    Console.WriteLine("\nContato adicionado!");

                }

            }
            else
            {
                Console.WriteLine("\nNão foi possivel adicionar aos contatos");
            }
        }

        //-----------------------------------------------------------------------
        //Lista elementos do arquivo .txt
        protected static List<Contato> listar(String caminho)
        {
            List<Contato> contatos = new List<Contato>();

            if (File.Exists(caminho))
            {
                using (StreamReader sr = new StreamReader(caminho))
                {
                    String linha;

                    int linhas = 0;

                    while ((linha = sr.ReadLine()) != null)
                    {
                        String[] elementos = linha.Split(",");

                        if (elementos.Length == 3)
                        {
                            Contato contato = new Contato { nome = elementos[0], telefone = elementos[1], email = elementos[2] };
                            contatos.Add(contato);
                            linhas++;
                        }
                    }

                    if (linhas == 0)
                    {
                        Console.WriteLine("\nNão há contatos na lista");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nO arquivo não existe.");
            }
            return contatos;
        }

        //-----------------------------------------------------------------------
        // Classe padrão de exibição dos contatos 
        protected class ContatoFormatter : IContatoFormatter
        {

            public virtual void ExibirContatos(List<Contato> contatos)
            {
                foreach (Contato contato in contatos)
                {
                    Console.WriteLine($"{contato.nome} - {contato.telefone} - {contato.email}");
                }
            }
        }


        //-----------------------------------------------------------------------
        // Subclass de ContatoFormatter - exibição em Markdown
        protected class MarkdownFormatter : ContatoFormatter
        {

            public override void ExibirContatos(List<Contato> contatos)
            {
                Console.WriteLine("\n## Lista de Contatos");

                foreach (Contato contato in contatos)
                {
                    Console.WriteLine($"- **Nome:** {contato.nome}\n" +
                        $"- Telefone: {contato.telefone}\n" +
                        $"- Email: {contato.email}");
                }
            }

        }


        //-----------------------------------------------------------------------
        // Subclass de ContatoFormatter - exibição em tabela
        protected class TabelaFormatter : ContatoFormatter
        {
            public override void ExibirContatos(List<Contato> contatos)
            {
                Console.WriteLine("\n----------------------------------------\n" +
                    "| Nome | Telefone | Email |\n" +
                    "----------------------------------------");

                foreach (Contato contato in contatos)
                {
                    Console.WriteLine($"|{contato.nome} | {contato.telefone} | {contato.email}");
                }
                Console.WriteLine("----------------------------------------\n");
            }
        }


        //-----------------------------------------------------------------------
        // Subclass de ContatoFormatter - exibição em linha
        protected class RawTextFormatter : ContatoFormatter
        {
            public override void ExibirContatos(List<Contato> contatos)
            {
                Console.WriteLine("\n");
                foreach (Contato contato in contatos)
                {
                    Console.WriteLine($"Nome: {contato.nome} | Telefone: {contato.telefone} | Email: {contato.email}");
                }
            }
        }
    }
    //-----------------------------------------------
    //class utilizada para instanciar usuario
    internal class Contato
    {
        public String nome;
        public String telefone;
        public String email;

    }

}
