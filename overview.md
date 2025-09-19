## How frontend and backend would workout :
- Backend (.NET Core) in Visual Studio , exposes our APIs
- Frontend (React + Tailwind) in VS Code , will use FetchAPI/Axios lib for backend API call
- **CORS Configuration** : in our Program.cs code we need to add CORS config ,
  
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:****")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

app.UseCors("AllowFrontend");
```
- src/     // APPROX STRUCTURE MAY VARY      
 ├── components/             
 │    ├── RiskList.jsx             
 │    ├── MitigationList.jsx             
 │    ├── MitigationForm.jsx             
 │    ├── RiskReport.jsx            
 │    └── OrgProfile.jsx            
 ├── pages/             
 │    ├── Dashboard.jsx                 
 │    ├── Login.jsx              
 │    ├── Register.jsx            
 ├── services/            
 │    ├── api.js   // central API calls           
 ├── App.jsx            
 └── index.css (with Tailwind)                    
