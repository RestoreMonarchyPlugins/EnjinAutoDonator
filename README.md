<img src="icon.png" height="125" width="125" />

# EnjinAutoDonator
Free and open source Enjin auto donations plugin for Unturned  
**This plugin is sponsored by Rise and RisenHero.net**

Download latest version from [Github Releases](https://github.com/RestoreMonarchyPlugins/EnjinAutoDonator/releases)  
Please post bug reports and feature requests in [Github Issues](https://github.com/RestoreMonarchyPlugins/EnjinAutoDonator/issues)

You can download `WebDonationsToEnjinAutoDonator.zip` in the releases that has exec file which can migrate WebDonations config to EnjinAutoDonator easily.
### How it works?
* Every `RefreshTime` the plugin is downloading `DaysBack` purchases from enjin website. 
* It uses `ServerIdentifier` to check if purchase was already processed on this server. 
* If it wasn't processed on this server yet it will look for the `ShopItemFeatures` with downloaded `EnjinItemID` in configuration. 
* If it doesn't exist in the configuration the plugin will skip the execute method, but it will still log this purchase to `PurchasesTable`.  
* Purchase is logged to Discord only once, when it's processed for the first time on any server.

### Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<EnjinAutoDonatorConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>magenta</MessageColor> <!-- Color of chat messages -->
  <ServerIdentifier>PVP1</ServerIdentifier> <!-- Any string (max 50 characters) that will be ID of current server -->
  <WebsiteUrl>http://yourwebsite.enjin.com</WebsiteUrl> <!-- The root url of your Enjin website --> 
  <APIKey>APIKEY</APIKey> <!-- APIKey of your Enjin website -->
  <PresetId>41012001</PresetId> <!-- ID of your Enjin Donation Store -->
  <DiscordWebhookUrl></DiscordWebhookUrl> <!-- URL of your Discord webhook, leave empty to disable -->
  <DiscordWebhookColor>#843da4</DiscordWebhookColor> <!-- The HEX color of your Discord webhook embed border color -->
  <LookDaysBack>9</LookDaysBack> <!-- Number of days back to look for unprocessed purchases -->
  <RefreshTimeMiliseconds>30000</RefreshTimeMiliseconds> <!-- Refresh time of checking for new purchases (in miliseconds) -->
  <RequestTimeoutMiliseconds>10000</RequestTimeoutMiliseconds> <!-- Your enjin site web request timeout -->
  <PayUconomyMoneyOnce>true</PayUconomyMoneyOnce> <!-- Set this to true if you don't want uconomy to increase player balance on every server -->
  <DatabaseAddress>127.0.0.1</DatabaseAddress> <!-- Your MySQL database address -->
  <DatabaseUsername>root</DatabaseUsername> <!-- Your MySQL database username -->
  <DatabasePassword>password123</DatabasePassword> <!-- Your MySQL database user password -->
  <DatabaseName>unturned</DatabaseName> <!-- Your MySQL database name -->
  <DatabasePort>3306</DatabasePort> <!-- Your MySQL database port -->
  <PurchasesTableName>FinishedPurchases</PurchasesTableName> <!-- Name of Purchases database table -->
  <SteamIDIdentifier>steamID</SteamIDIdentifier> <!-- The variable name for steam ID in your enjin site -->
  <Features>
    <ShopItemFeatures EnjinItemId="3024925" DisableBroadcast="false"> <!-- EnjinItemId is ID of your Enjin donation store item -->
      <AddGroups>
        <GroupID>star</GroupID> <!-- Rocket permissions group ID to be added for player -->
      </AddGroups>
      <RemoveGroups>
        <GroupID>vip</GroupID> <!-- Rocket permissions group ID to be removed from player -->
      </RemoveGroups>
      <Commands>
        <Command>/airdrop</Command> <!-- Any command to execute, can use variables {steamid} and {steamname} -->
      </Commands>
      <UconomyMoney>300</UconomyMoney> <!-- Amount of money to be paid to player via Uconomy, make 0 to not pay any money --> 
    </ShopItemFeatures>
  </Features>
</EnjinAutoDonatorConfiguration>
```
