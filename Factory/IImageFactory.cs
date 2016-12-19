using System.Drawing;
using System.Threading.Tasks;

namespace chumakeveryday.Factory
{
    public interface IImageFactory
    {
        Task<Image> GenerateRandom();
        Task<Image> GenerateDaily();
    }
}