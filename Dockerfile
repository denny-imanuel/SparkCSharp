FROM 3rdman/dotnet-spark:latest
WORKDIR /SparkCSharp
ENV SPARK_LOCAL_IP="0.0.0.0"

COPY . .

RUN dotnet build && wait
RUN spark-submit \
    --class org.apache.spark.deploy.dotnet.DotnetRunner \
    --master local bin/Debug/netcoreapp3.1/microsoft-spark-3-1_2.12-2.0.0.jar  \
    dotnet \
    bin/Debug/netcoreapp3.1/SparkCSharp.dll
