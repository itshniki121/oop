using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Service;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupTest
{
    [Fact]
    public void Test()
    {
        var fs = new MemoryFileSystem();
        var backup = new Backup();
        var repository = new Repository(new ZipArchiver(), fs, "/mnt/c/repository");
        var task = new BackupTask(new SingleStorer(), backup, repository);
        fs.CreateDirectory("/mnt/c/dir1");
        fs.CreateDirectory("/mnt/c/dir1/dir2");
        fs.CreateDirectory("/mnt/c/dir1/dir3");
        fs.CreateFile("/mnt/c/dir1/123.txt").Dispose();
        fs.CreateFile("/mnt/c/dir1/dir2/info.log").Dispose();
        fs.CreateFile("/mnt/c/dir1/dir2/picture.png").Dispose();
        task.AddBackupFile("/mnt/c/dir1/dir2/info.log");
        task.AddBackupFile("/mnt/c/dir1/dir2/picture.png");
        task.AddBackupFolder("/mnt/c/dir1/dir3");
        task.Run();
        Assert.Equal(1, backup.RestorePoints.Count);
    }

    [Fact]
    public void Test2()
    {
        var fs = new PhysicalFileSystem();
        var backup = new Backup();
        fs.CreateDirectory("/mnt/c/repository");
        var repository = new Repository(new ZipArchiver(), fs, fs.ConvertPathFromInternal(@"C:\Users\dmitr\Desktop\Repo"));
        var task = new BackupTask(new SingleStorer(), backup, repository);
        task.AddBackupFile(fs.ConvertPathFromInternal(@"C:\Users\dmitr\Desktop\Lab_3_13.pdf").FullName);
        task.AddBackupFolder(fs.ConvertPathFromInternal(@"C:\Users\dmitr\Desktop\Files").FullName);
        task.Run();
    }
}