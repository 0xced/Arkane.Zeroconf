#region header

// Arkane.ZeroConf - ServiceBrowser.cs
// 

#endregion

#region using

using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Runtime.CompilerServices ;
using System.Threading ;
using ArkaneSystems.Arkane.Zeroconf.Providers ;

#endregion

namespace ArkaneSystems.Arkane.Zeroconf ;

public class ServiceBrowser : IServiceBrowser
{
    public ServiceBrowser () =>
        this.browser = (IServiceBrowser) Activator.CreateInstance (ProviderFactory.SelectedProvider.ServiceBrowser) ;

    private readonly IServiceBrowser browser ;

    public void Dispose () { this.browser.Dispose () ; }

    public void Browse (uint interfaceIndex, AddressProtocol addressProtocol, string regtype, string domain)
    {
        this.browser.Browse (interfaceIndex, addressProtocol, regtype, domain ?? "local") ;
    }

    public IEnumerator <IResolvableService> GetEnumerator () => this.browser.GetEnumerator () ;

    IEnumerator IEnumerable.GetEnumerator () => this.browser.GetEnumerator () ;

    public event ServiceBrowseEventHandler ServiceAdded
    {
        add => this.browser.ServiceAdded += value ;
        remove => this.browser.ServiceRemoved -= value ;
    }

    public event ServiceBrowseEventHandler ServiceRemoved
    {
        add => this.browser.ServiceRemoved += value ;
        remove => this.browser.ServiceRemoved -= value ;
    }

    public void Browse (uint interfaceIndex, string regtype, string domain)
    {
        this.Browse (interfaceIndex, AddressProtocol.Any, regtype, domain) ;
    }

    public void Browse (AddressProtocol addressProtocol, string regtype, string domain)
    {
        this.Browse (0, addressProtocol, regtype, domain) ;
    }

    public void Browse (string regtype, string domain) { this.Browse (0, AddressProtocol.Any, regtype, domain) ; }

    public async IAsyncEnumerable<IResolvableService> BrowseAsync (uint interfaceIndex, AddressProtocol addressProtocol, string regtype, string domain, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var service in this.browser.BrowseAsync (interfaceIndex, addressProtocol, regtype, domain ?? "local", cancellationToken))
        {
            yield return service;
        }
    }

    public async IAsyncEnumerable<IResolvableService> BrowseAsync (uint interfaceIndex, string regtype, string domain, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var service in this.BrowseAsync (interfaceIndex, AddressProtocol.Any, regtype, domain, cancellationToken))
        {
            yield return service;
        }
    }

    public async IAsyncEnumerable<IResolvableService> BrowseAsync (AddressProtocol addressProtocol, string regtype, string domain, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var service in this.BrowseAsync (0, addressProtocol, regtype, domain, cancellationToken))
        {
            yield return service;
        }
    }

    public async IAsyncEnumerable<IResolvableService> BrowseAsync(string regtype, string domain, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var service in this.BrowseAsync (0, AddressProtocol.Any, regtype, domain, cancellationToken))
        {
            yield return service;
        }
    }
}
