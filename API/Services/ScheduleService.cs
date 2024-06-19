using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ScheduleService
    {
        private Database.Context _context;
        public ScheduleService(Database.Context context)
        {
            _context = context;
        }

        public IEnumerable<Schedule> Get()
        {
            return _context.Schedules.Include(s => s.ScheduleStatus).Include(s => s.Lessons);
        }
        public void ActivateSchedule(int scheduleId)
        {
            foreach (var item in _context.Schedules.Where(s => s.ScheduleStatusId == 3))
            {
                item.ScheduleStatusId = 1;
            }
            Schedule? activating = _context.Schedules.Where(sch => sch.Id == scheduleId).FirstOrDefault();
            if (activating == null)
            {
                throw new InvalidDataException("Расписание не найдено");
            }
            _context.SaveChanges();
        }
        public void ChangeStatus(int scheduleId, int statusId)
        {
            Schedule? changing = _context.Schedules.Where(sch => sch.Id == scheduleId).FirstOrDefault();
            if (changing == null)
            {
                throw new InvalidDataException("Расписание не найдено");
            }

            if (changing.ScheduleStatusId == statusId)
            {
                return;
            }

            if (!_context.ScheduleStatuses.Any(s=>s.Id == statusId))
            {
                throw new InvalidDataException("Некорректный статус");
            }

            changing.ScheduleStatusId = statusId;
            _context.SaveChanges();
        }
        public void HandleChange(int scheduleId)
        {
            Schedule? changing = _context.Schedules.Where(sch => sch.Id == scheduleId).FirstOrDefault();
            if (changing == null)
            {
                throw new InvalidDataException("Расписание не найдено");
            }
            changing.LastChange = DateTime.Now;

            if (changing.ScheduleStatusId == 3)
            {
                return;
            }
            changing.ScheduleStatusId = 2;
            _context.SaveChanges();
        }
    }
}
