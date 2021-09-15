using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace cli.Operations {
  public class PbixImporter : IRun {

    public void Run(CliOptions opts) { 
      if (String.IsNullOrEmpty(opts.File)) throw new Exception($"the import command requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File)) { throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\""); }
      var dir = Directory.CreateDirectory(opts.Dir);
      var backup = Directory.CreateDirectory(Path.Combine(dir.FullName, "backups"));
      BackupPbixFile(opts.File, backup.FullName);

      ZipFile.ExtractToDirectory(opts.File, dir.FullName, true);
      CreateGitIgnoreFile(dir);
      FormatFiles(dir);
      MoveDataModelFile(dir, opts.DataModelName);

      DeleteExpiredBackupFiles(backup.FullName);
      
      Console.WriteLine($"source directory [{dir.Name}] created and all PBIX files extracted");
    }

    private void BackupPbixFile(string file, string dir) { 
      
      var name = new FileInfo(file).Name;
      var path = Path.Combine(dir, name.Replace(".pbix", $"_{DateTime.Now:yyyyMMdd HHmm}.pbix"));
      File.Delete(path);
      File.Copy(file, path);
    }

    private void CreateGitIgnoreFile(DirectoryInfo dir) { 
      var path = Path.Combine(dir.FullName, Constants.GITIGNORE);
      if (File.Exists(path)) return;
      
      var contents = String.Join("\n", Constants.BINARIES) + "\ndata\\\nbackups\\";
      File.WriteAllText(path, contents);
    }

    private void FormatFiles(DirectoryInfo dir) { 
      dir.GetFiles("*.*", SearchOption.AllDirectories).
          Where(PbixHelpers.IsValidSourceFile).
          ToList().
          ForEach(f => File.WriteAllText(f.FullName, FormatFileContents(f), Encoding.UTF8));
    }

    private string FormatFileContents(FileInfo f) {
      var encoder = PbixHelpers.GetFileEncoding(f);
      var contents = File.ReadAllText(f.FullName, encoder);
      return PbixHelpers.FormatFileContentsImpl(f, contents, true);
    }

    private void MoveDataModelFile(DirectoryInfo dir, string name)
    {
      var datadir = dir.CreateSubdirectory(Constants.DATA_DIR);
      var dest = Path.Combine(datadir.FullName, name);
      if (File.Exists(dest)) File.Delete(dest);
      File.Move(Path.Combine(dir.FullName, "DataModel"), dest);
    }

    private void DeleteExpiredBackupFiles(string dir) { 
      new DirectoryInfo(dir).GetFiles("*.pbix").Where(f => (DateTime.Now - f.CreationTime).TotalDays > 5).ToList().ForEach(f => f.Delete());
    }
  }
}