using AutoMapper;
using HRRecruitingApp.Data;
using HRRecruitingApp.DTOs;
using HRRecruitingApp.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitingApp.Services;

public class CVService : ICVService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public CVService(AppDbContext db, IMapper mapper, IWebHostEnvironment env)
    {
        _db = db;
        _mapper = mapper;
        _env = env;
    }

    public async Task<CVUploadResultDto> UploadAsync(IFormFile file)
    {
        var storage = Path.Combine(_env.ContentRootPath, "cv-storage");
        Directory.CreateDirectory(storage);
        var fileName = $"{Guid.NewGuid()}.pdf";
        var path = Path.Combine(storage, fileName);

        using (var stream = File.Create(path))
        {
            await file.CopyToAsync(stream);
        }

        // Extract text to get name (very basic)
        string? name = null;
        using (var reader = new PdfReader(path))
        using (var doc = new PdfDocument(reader))
        {
            var text = PdfTextExtractor.GetTextFromPage(doc.GetPage(1));
            name = text.Split('\n').FirstOrDefault();
        }

        var candidate = new Candidate { Name = name ?? "Sin nombre" };
        var cv = new CV { FilePath = fileName, Candidate = candidate };

        _db.Candidates.Add(candidate);
        _db.CVs.Add(cv);
        await _db.SaveChangesAsync();

        return new CVUploadResultDto(candidate.Id, cv.Id);
    }
}
