using UI;
HttpService httpService = new HttpService();
await new MainMenu(httpService).Start();