using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.Display;

namespace FullCalendarBlazor.Models
{
    public class FullCalendarData
    {
        #region Overall Display

        public Toolbar HeaderToolbar { get; set; }
        public Toolbar FooterToolbar { get; set; }
        public DateTimeFormatter TitleFormat { get; set; }
        public string TitleRangeSeparator { get; set; }
        public Dictionary<string, string> ButtonText { get; set; }
        public Dictionary<string, string> ButtonIcons { get; set; }
        public object CustomButtons { get; set; }
        public string ThemeSystem { get; set; }
        public string Height { get; set; }
        public string ContentHeight { get; set; }
        public double? AspectRatio { get; set; }
        public bool? ExpandRows { get; set; }
        public bool? HandleWindowResize { get; set; }
        public int? WindowResizeDelay { get; set; }
        public string StickyHeaderDates { get; set; }
        public string StickyFooterScrollbar { get; set; }

        #endregion

        #region Views

        public bool? FixedWeekCount { get; set; }
        public bool? ShowNonCurrentDates { get; set; }
        public int? EventMinHeight { get; set; }
        public int? EventShortHeight { get; set; }
        public bool? SlotEventOverlap { get; set; }
        public bool? AllDaySlot { get; set; }
        public DateTimeFormatter ListDayFormat { get; set; }
        public DateTimeFormatter ListDaySideFormat { get; set; }
        public string InitialView { get; set; }

        #endregion

        #region Date and Time

        public bool? Weekends { get; set; }
        public IEnumerable<DayOfWeek> HiddenDays { get; set; }
        public bool? DayHeaders { get; set; }
        public DateTimeFormatter DayHeaderFormat { get; set; }
        public int? DayMinWidth { get; set; }
        public TimeSpan? SlotDuration { get; set; }
        public TimeSpan? SlotLabelInterval { get; set; }
        public DateTimeFormatter SlotLabelFormat { get; set; }
        public TimeSpan? SlotMinTime { get; set; }
        public TimeSpan? SlotMaxTime { get; set; }
        public TimeSpan? ScrollTime { get; set; }
        public bool? ScrollTimeReset { get; set; }
        public DateTime? InitialDate { get; set; }
        public TimeSpan? DateIncrement { get; set; }
        public string DateAlignment { get; set; }
        public DateRange ValidRange { get; set; }
        public bool? NavLinks { get; set; }
        public string NavLinkDayClick { get; set; } // Todo: Could be a function
        public string NavLinkWeekClick { get; set; } // Todo: Could be a function
        public bool? WeekNumbers { get; set; }
        public string WeekNumberCalculation { get; set; }
        public string WeekText { get; set; }
        public DateTimeFormatter WeekNumberFormat { get; set; }
        public bool? Selectable { get; set; }
        public bool? SelectMirror { get; set; }
        public bool? UnselectAuto { get; set; }
        public string UnselectCancel { get; set; }
        public bool? SelectOverlap { get; set; }
        public object SelectConstraint { get; set; }
        public int? SelectMinDistance { get; set; }
        public bool? NowIndicator { get; set; }
        public DateTime? Now { get; set; }
        public object BusinessHours { get; set; }

        #endregion

        #region Events

        public IEnumerable<Event> Events { get; set; }
        public bool? DefaultAllDay { get; set; }
        public TimeSpan? DefaultAllDayEventDuration { get; set; }
        public TimeSpan? DefaultTimedEventDuration { get; set; }
        public bool? ForceEventDuration { get; set; }
        public string EventColor { get; set; }
        public string EventBackgroundColor { get; set; }
        public string EventBorderColor { get; set; }
        public string EventTextColor { get; set; }
        public string EventDisplay { get; set; }
        public DateTimeFormatter EventTimeFormat { get; set; }
        public bool? DisplayEventTime { get; set; }
        public bool? DisplayEventEnd { get; set; }
        public TimeSpan? NextDayThreshold { get; set; }
        public IEnumerable<string> EventOrder { get; set; }
        public bool? EventOrderStrict { get; set; }
        public bool? ProgressiveEventRendering { get; set; }
        public bool? Editable { get; set; }
        public bool? EventStartEditable { get; set; }
        public bool? EventResizableFromStart { get; set; }
        public bool? EventDurationEditable { get; set; }
        public bool? EventResourceEditable { get; set; }
        public bool? Droppable { get; set; }
        public int? EventDragMinDistance { get; set; }
        public int? DragRevertDuration { get; set; }
        public bool? DragScroll { get; set; }
        public TimeSpan? SnapDuration { get; set; }
        public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent property
        public object EventConstraint { get; set; }
        public int? DayMaxEventRows { get; set; }
        public int? DayMaxEvents { get; set; }
        public int? EventMaxStack { get; set; }
        public string MoreLinkClick { get; set; } // Todo: Could be a function
        public DateTimeFormatter DayPopoverFormat { get; set; }

        #endregion
    }
}