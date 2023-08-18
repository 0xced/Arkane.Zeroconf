## Conclusion: it's impossible to get it to work on macOS + Docker

See [How do I advertise AND browse mDNS from within docker container?](https://stackoverflow.com/questions/44078097/how-do-i-advertise-and-browse-mdns-from-within-docker-container)

```sh
cd Arkane.Zeroconf
set pwd=/%cd:\=/%
docker run --network host --interactive --tty --rm --volume "%pwd::=%:/home" mcr.microsoft.com/dotnet/sdk:6.0
```

```sh
apt update
apt install binutils -y
apt install libavahi-compat-libdnssd1 -y
apt install avahi-utils -y
apt install net-tools -y
apt install iputils-ping -y
apt install procps -y
apt install nano -y
```

```sh
nm --dynamic --defined-only /usr/lib/x86_64-linux-gnu/libdns_sd.so.1
```

```
0000000000006f90 T DNSServiceAddRecord
0000000000005380 T DNSServiceBrowse
0000000000005890 T DNSServiceConstructFullName
0000000000006f60 T DNSServiceCreateConnection
0000000000005900 T DNSServiceEnumerateDomains
0000000000005010 T DNSServiceProcessResult
0000000000006140 T DNSServiceQueryRecord
0000000000006f30 T DNSServiceReconfirmRecord
0000000000005140 T DNSServiceRefDeallocate
0000000000004fe0 T DNSServiceRefSockFD
0000000000005b00 T DNSServiceRegister
0000000000006f00 T DNSServiceRegisterRecord
0000000000006fc0 T DNSServiceRemoveRecord
0000000000005660 T DNSServiceResolve
0000000000005f40 T DNSServiceUpdateRecord
0000000000006ab0 T TXTRecordContainsKey
0000000000006590 T TXTRecordCreate
0000000000006610 T TXTRecordDeallocate
0000000000006a50 T TXTRecordGetBytesPtr
0000000000006ca0 T TXTRecordGetCount
0000000000006d50 T TXTRecordGetItemAtIndex
00000000000069d0 T TXTRecordGetLength
0000000000006b40 T TXTRecordGetValuePtr
0000000000006910 T TXTRecordRemoveValue
0000000000006670 T TXTRecordSetValue
0000000000006ff0 T avahi_exe_name
00000000000070d0 T avahi_warn
0000000000007290 T avahi_warn_linkage
0000000000007320 T avahi_warn_unsupported
```

```sh
service dbus start
Starting system message bus: dbus.
```

```sh
service avahi-daemon start
Starting Avahi mDNS/DNS-SD Daemon: avahi-daemon.
```

```sh
avahi-browse -a
```


```sh
cd /home/azclient
```

```sh
dotnet run -- --resolve --verbose
```

```
Creating a ServiceBrowser with the following settings:
  Interface         = 0 (All)
  Address Protocol  = Any
  Domain            = local
  Registration Type = _workstation._tcp
  Resolve Shares    = True

Hit ^C when you're bored waiting for responses.

*** WARNING *** The program 'azclient' uses the Apple Bonjour compatibility layer of Avahi.
*** WARNING *** Please fix your application to use the native API of Avahi!
*** WARNING *** For more information see <http://0pointer.de/blog/projects/avahi-compat.html>
*** WARNING *** The program 'azclient' called 'DNSServiceCreateConnection()' which is not supported (or only supported partially) in the Apple Bonjour compatibility layer of Avahi.
*** WARNING *** Please fix your application to use the native API of Avahi!
*** WARNING *** For more information see <http://0pointer.de/blog/projects/avahi-compat.html>
Unhandled exception. ArkaneSystems.Arkane.Zeroconf.Providers.Bonjour.ServiceErrorException: Unsupported
   at ArkaneSystems.Arkane.Zeroconf.Providers.Bonjour.Zeroconf.Initialize() in /home/Arkane.ZeroConf/Providers/Bonjour/ZeroconfProvider.cs:line 28
   at ArkaneSystems.Arkane.Zeroconf.Providers.Bonjour.ZeroconfProvider.Initialize() in /home/Arkane.ZeroConf/Providers/Bonjour/ZeroconfProvider.cs:line 42
   at ArkaneSystems.Arkane.Zeroconf.Providers.ProviderFactory.GetProviders() in /home/Arkane.ZeroConf/Providers/ProviderFactory.cs:line 56
   at ArkaneSystems.Arkane.Zeroconf.Providers.ProviderFactory.get_DefaultProvider() in /home/Arkane.ZeroConf/Providers/ProviderFactory.cs:line 29
   at ArkaneSystems.Arkane.Zeroconf.Providers.ProviderFactory.get_SelectedProvider() in /home/Arkane.ZeroConf/Providers/ProviderFactory.cs:line 37
   at ArkaneSystems.Arkane.Zeroconf.ServiceBrowser..ctor() in /home/Arkane.ZeroConf/ServiceBrowser.cs:line 24
   at ArkaneSystems.Arkane.Zeroconf.Client.MZClient.Main(String[] args) in /home/azclient/ZeroconfClient.cs:line 155
```

```
gh repo clone geekman/mdns-repeater

# SOL_IP trick from https://github.com/darkk/redsocks/issues/42#issue-12666120
CFLAGS=-Wall -DSOL_IP=IPPROTO_IP

make

docker network create --opt com.docker.network.bridge.name=docker_test test-net
```

