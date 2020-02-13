using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ZJ.App.Common;
using ZJ.App.Entity;
using ZJ.App.DAL;


namespace ZJ.App.BLL
{
    public partial class AlbumsBLL : BllBase
    {
        #region [Albums]
        public void AddAlbumsEntity(AlbumsEntity entity)
        {
            AlbumsDAL dal = new AlbumsDAL();
            dal.Insert(entity);
        }

        public int UpdateAlbumsEntity(AlbumsEntity entity)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.Update(entity);
        }

        /// <summary>AlbumsEntity软删除方法
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DisableAlbumsEntityById(object Id)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.Disabled(Id);
        }

        public int DeleteAlbumsEntityById(object Id)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.Delete(Id);
        }


        public AlbumsEntity GetAlbumsEntityById(object Id)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.GetEntityById(Id);
        }


        //返回全部AlbumsEntity
        public List<AlbumsEntity> GetAllAlbums(List<SqlDbParameter> parms)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.GetAll(parms);
        }

        public List<AlbumsEntity> GetAllAlbums(List<SqlDbParameter> parms, string orderBy)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.GetAll(parms, orderBy);
        }

        //列表分页查询
        public List<AlbumsEntity> GetAlbumsPaged(List<SqlDbParameter> parms, string OrderBy, int PageSize, int PageIndex, out int RecordCount)
        {
            AlbumsDAL dal = new AlbumsDAL();
            return dal.GetAll(parms, OrderBy, PageSize, PageIndex, out RecordCount);
        }

        public tdeptEntity GetTdeptEntityByID(object Id)
        {
            tdeptDAL dal = new tdeptDAL();
            return dal.GetEntityById(Id);
        }
        #endregion
    }
}
