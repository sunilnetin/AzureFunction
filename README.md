# AzureFunction
To Learn Basic Azure Function and Timer Trigger with azure storage emulator c#
#Make sure you have the following installed:
- .NET SDK
- Azure Functions Core Tools
- Visual Studio Code with the C# and Azure Functions extensions

#🚀 Create a New Azure Function Projec
<code>
func init MyFunctionProj --worker-runtime dotnet --target-framework net9.0
</code>
#✨ Add a Function to the Project
<code>
cd MyFunctionProj
func new --name HelloFunction --template "HTTP trigger" --authlevel "anonymous"
</code>

#🛠️ Build the Project
<code>
dotnet build
</code>
#▶️ Run the Function Locally
<code>
func start
</code>

#🧪 Test It
Open a browser or use curl:
<code>
curl http://localhost:7071/api/HelloFunction?name=Azure
</code>
