#region header

// Arkane.ZeroConf - IServiceBrowser.cs
// 

#endregion

#region using

using System ;
using System.Collections.Generic ;
using System.Threading ;

#endregion

namespace ArkaneSystems.Arkane.Zeroconf ;

public interface IServiceBrowser : IEnumerable <IResolvableService>, IDisposable
{
    event ServiceBrowseEventHandler ServiceAdded ;

    event ServiceBrowseEventHandler ServiceRemoved ;

    void Browse (uint interfaceIndex, AddressProtocol addressProtocol, string regtype, string domain) ;

    IAsyncEnumerable<IResolvableService> BrowseAsync (uint interfaceIndex, AddressProtocol addressProtocol, string regtype, string domain, CancellationToken cancellationToken) ;
}
