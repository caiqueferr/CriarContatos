using System;
using System.Collections.Generic;

namespace CriacaoDeContatos
{
    public interface IContatoFormatter
    {
        void ExibirContatos(List<Contato> contatos);
    }
}
