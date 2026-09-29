using System;
using System.Collections.Generic;

namespace CriacaoDeContatos
{
    internal interface IContatoFormatter
    {
        void ExibirContatos(List<Contato> contatos);
    }
}
