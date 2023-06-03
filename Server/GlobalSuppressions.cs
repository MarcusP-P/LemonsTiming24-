// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Generated file", Scope = "member", Target = "~F:LemonsTiming24.Server.Controllers.WeatherForecastController.logger")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Generated file", Scope = "member", Target = "~F:LemonsTiming24.Server.Pages.ErrorModel.logger")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "The logger will be useful", Scope = "member", Target = "~F:LemonsTiming24.Server.Services.BackgroundProcessing.TimingDataFetcher.logger")]
[assembly: SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "This class is instantiated through DI", Scope = "type", Target = "~T:LemonsTiming24.Server.Infrastructure.SocketIO.HttpClientRequestTrace")]
[assembly: SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "These are all DTOs", Scope = "namespaceanddescendants", Target = "N:LemonsTiming24.Server.Model.RawTiming")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "These are all DTOs", Scope = "namespaceanddescendants", Target = "N:LemonsTiming24.Server.Model.RawTiming")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "These are all DTOs", Scope = "namespaceanddescendants", Target = "N:LemonsTiming24.Server.Model.Database.RawData")]
