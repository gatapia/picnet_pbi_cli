﻿using System;
using System.IO;
using System.Linq;
using Ionic.Zip;

namespace cli.Operations
{
  public class PbixDataExtract : IRun 
  {

    public void Run(CliOptions opts)
    {
      if (String.IsNullOrEmpty(opts.File)) throw new Exception("the data command requires the -f file_name.pbix parameter");
      if (!File.Exists(opts.File)) { throw new FileNotFoundException($"could not find the specified pbix file \"{opts.File}\""); }
      var dir = Directory.CreateDirectory(opts.Output ?? Constants.DEFAULT_SRC_DIR);
      var datadir = dir.CreateSubdirectory(Constants.DATA_DIR);
      using var f = ZipFile.Read(opts.File);
      f.Entries.Single(e => e.FileName == "DataModel").Extract(datadir.FullName);
      File.Move(Path.Combine(datadir.FullName, "DataModel"), Path.Combine(datadir.FullName, opts.DataModelName));

      Console.WriteLine($"data model file created in [{dir.Name}/{datadir.Name}/{opts.DataModelName}] created and all PBIX files extracted");
    }
  }
}