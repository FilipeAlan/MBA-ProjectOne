﻿
namespace Blog.Data.Interface
{
    public interface IRepositorio<T> where T : class
    {   
        Task<int> Adicionar(T entidade);
        Task<int> Atualizar(T entidade);
        Task<int> Deletar(T entidade);
        Task<int> Deletar(int id);
    }
}
