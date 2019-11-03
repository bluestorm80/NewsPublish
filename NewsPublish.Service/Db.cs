using Microsoft.EntityFrameworkCore;
using NewsPublish.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPublish.Service
{
    /// <summary>
    /// 数据库访问上下文
    /// </summary>
    public class Db:DbContext
    {
        public Db() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=WIN-CHPVENJM68R\SQLEXPRESS;Initial Catalog=NewsPublish;User ID=sa;Password=123456",
                b=>b.UseRowNumberForPaging());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsClassify> NewsClassify { get; set; }
        public virtual DbSet<NewsComment> NewsComment { get; set; }
    }
}
