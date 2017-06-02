using System;

namespace Soedeum.Dotnet.Library.Collections
{
    public class FetcherSimpleScanner<T> : BaseSimpleScanner<T>
    {
        IFetcher<T> fetcher;


        public FetcherSimpleScanner(IFetcher<T> fetcher)
        {
            this.fetcher = fetcher;
        }

        public override void Dispose() => fetcher.Dispose();

        protected override bool GetIsEnd(T current) => fetcher.IsEnd(current);

        protected override T FetchInitial()
        {
            return fetcher.FetchInitial();
        }

        protected override T FetchNext(T previous)
        {
            return fetcher.FetchNext(previous);
        }

    }
}