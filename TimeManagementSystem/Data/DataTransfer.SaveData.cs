using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeManagementSystem.Objects;

namespace TimeManagementSystem.Data
{
    /// <summary>
    /// Save data class
    /// </summary>
    public static partial class DataTransfer
    {
        /// <summary>
        /// Save object to DB
        /// </summary>
        /// <param name="saveObject"></param>
        /// <returns></returns>
        public static bool Save(this ISaveObject saveObject, CommandAttribute commandAttribute)
        {
            if (saveObject is TaskEvent)
                return SaveObject((TaskEvent)saveObject, commandAttribute);
            else if (saveObject is AppointmentEvent)
                return SaveObject((AppointmentEvent)saveObject, commandAttribute);
            else if (saveObject is Contact)
                return SaveObject((Contact)saveObject, commandAttribute);

            return false;
        }

        /// <summary>
        /// Save object TaskEvent
        /// </summary>
        /// <param name="saveObject"></param>
        /// <param name="commandAttribute"></param>
        /// <returns></returns>
        private static bool SaveObject(TaskEvent saveObject, CommandAttribute commandAttribute)
        {
            int affectedRows = 0;

            using (var conn = OpenConnection(ConnectionString))
            {
                switch (commandAttribute)
                {
                    case CommandAttribute.INSERT:
                        {
                            try
                            {
                                var insertSQL = "INSERT INTO t_events (name, description, year, month, date_regdate, time_regdate, reg_date, event_type, rec_date, del_rec) Values (@Name, @Description, @Year, @Month, @Date, @Time, @RegDate, @ActionType, @RecDate, @DelRec);";
                                affectedRows = conn.Execute(insertSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error writing to database: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.UPDATE:
                        {
                            try
                            {
                                var updateSQL = "UPDATE public.t_users SET workplace_id = @WorkplaceId, department_id = @DepartmentId, username = @UserName, password = @Password, status = @Status, access_level_value = @AccessLevelValue, rec_date = now(), del_rec = @DelRec WHERE id = @Id ;";
                                affectedRows = conn.Execute(updateSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка обновления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.DELETE:
                        {
                            try
                            {
                                var deleteSQL = "DELETE FROM public.t_users WHERE id = @Id;";
                                affectedRows = conn.Execute(deleteSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка удаления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (affectedRows > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Save object AppointmentEvent
        /// </summary>
        /// <param name="saveObject"></param>
        /// <param name="commandAttribute"></param>
        /// <returns></returns>
        private static bool SaveObject(AppointmentEvent saveObject, CommandAttribute commandAttribute)
        {
            int affectedRows = 0;

            using (var conn = OpenConnection(ConnectionString))
            {
                switch (commandAttribute)
                {
                    case CommandAttribute.INSERT:
                        {
                            try
                            {
                                var insertSQL = "INSERT INTO t_events (name, description, year, month, date_regdate, time_regdate, reg_date, event_type, contact_id, location, rec_date, del_rec) Values (@Name, @Description, @Year, @Month, @Date, @Time, @RegDate, @ActionType, @ContactId, @Location, @RecDate, @DelRec);";
                                affectedRows = conn.Execute(insertSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error writing to database: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.UPDATE:
                        {
                            try
                            {
                                var updateSQL = "UPDATE public.t_users SET workplace_id = @WorkplaceId, department_id = @DepartmentId, username = @UserName, password = @Password, status = @Status, access_level_value = @AccessLevelValue, rec_date = now(), del_rec = @DelRec WHERE id = @Id ;";
                                affectedRows = conn.Execute(updateSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка обновления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.DELETE:
                        {
                            try
                            {
                                var deleteSQL = "DELETE FROM public.t_users WHERE id = @Id;";
                                affectedRows = conn.Execute(deleteSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка удаления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (affectedRows > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Save object Contact
        /// </summary>
        /// <param name="saveObject"></param>
        /// <param name="commandAttribute"></param>
        /// <returns></returns>
        private static bool SaveObject(Contact saveObject, CommandAttribute commandAttribute)
        {
            int affectedRows = 0;

            using (var conn = OpenConnection(ConnectionString))
            {
                switch (commandAttribute)
                {
                    case CommandAttribute.INSERT:
                        {
                            try
                            {
                                var insertSQL = "INSERT INTO t_contacts (name, phone, email, rec_date, del_rec) Values (@Name, @Phone, @EMail, @RecDate, @DelRec);";
                                affectedRows = conn.Execute(insertSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error writing to database: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.UPDATE:
                        {
                            try
                            {
                                var updateSQL = "UPDATE public.t_users SET workplace_id = @WorkplaceId, department_id = @DepartmentId, username = @UserName, password = @Password, status = @Status, access_level_value = @AccessLevelValue, rec_date = now(), del_rec = @DelRec WHERE id = @Id ;";
                                affectedRows = conn.Execute(updateSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка обновления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    case CommandAttribute.DELETE:
                        {
                            try
                            {
                                var deleteSQL = "DELETE FROM public.t_users WHERE id = @Id;";
                                affectedRows = conn.Execute(deleteSQL, saveObject);
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Ошибка удаления записи в БД: " + Environment.NewLine + exc.Message);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (affectedRows > 0)
                return true;

            return false;
        }
    }
}
