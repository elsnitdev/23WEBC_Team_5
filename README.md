# Cấu trúc thư mục dự án

```
├── # README - Cấu trúc thư mục dự án ASP.NET Core
├── ## Cấu trúc thư mục
├── wwwroot: Tài nguyên tĩnh phục vụ client./
│   ├── admins: Tài nguyên cho admin./
│   │   ├── css: CSS cho admin.
│   │   └── js: JavaScript cho admin.
│   ├── users: Tài nguyên cho người dùng./
│   │   ├── css: CSS cho người dùng.
│   │   └── js: JavaScript cho người dùng.
│   ├── fonts: Font chữ.
│   ├── lib: Thư viện bên thứ ba.
│   ├── uploads: Tệp tải lên.
│   └── favicon.ico: Biểu tượng favicon.
├── Areas: Phân tách module chức năng./
│   ├── Admins: Khu vực admin./
│   │   ├── Controllers: Controller admins.
│   │   ├── Middlewares: Middleware admins tùy chỉnh.
│   │   ├── Models: Model dữ liệu cho view admins.
│   │   ├── Services: dịch vụ admin.
│   │   └── Views: Giao diện.
│   │      └── Shared: View dùng chung.
│   └── Api: Khu vực api/
│       ├── Controllers: Controller api.
│       ├── Models: Model dữ liệu
│       └── Services: dịch vụ của api
├── Models: Model dữ liệu cho view.
├── Controllers: Controller chính.
├── Middlewares: Middleware tùy chỉnh.
├── Views: Giao diện.
│   └── Shared: View dùng chung.
├── Program.cs: Khởi chạy web.
└── appsettings.json: Cấu hình web.
```
