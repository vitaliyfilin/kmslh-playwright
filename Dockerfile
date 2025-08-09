
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install dependencies for Playwright
RUN apt-get update && \
    apt-get install -y libnss3 libatk1.0-0 libatk-bridge2.0-0 libcups2 libxcomposite1 libxrandr2 libxdamage1 libxss1 libasound2 libgbm1 libpangocairo-1.0-0 libgtk-3-0 gnupg && \
    rm -rf /var/lib/apt/lists/*

# Install Node & Playwright browsers
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && apt-get install -y nodejs && \
    npm i -g playwright && playwright install --with-deps && rm -rf /root/.npm /root/.cache

WORKDIR /src
COPY . /src

RUN dotnet restore tests/Kmslh.Tests.csproj
CMD ["dotnet", "test", "tests/Kmslh.Tests.csproj"]
