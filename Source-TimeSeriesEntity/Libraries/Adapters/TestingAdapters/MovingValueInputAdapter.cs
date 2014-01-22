﻿//******************************************************************************************************
//  MovingValueInputAdapter.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  01/10/2014 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using GSF;
using GSF.TimeSeries;
using GSF.TimeSeries.Adapters;

namespace TestingAdapters
{
    /// <summary>
    /// Represents a class used to stream a series of values that move randomly but change gradually.
    /// </summary>
    [Description("Moving Value: Generates a series of values that move randomly but change gradually.")]
    public class MovingValueInputAdapter : InputAdapterBase
    {
        #region [ Members ]

        // Constants

        /// <summary>
        /// Default value for the <see cref="MinValue"/> property.
        /// </summary>
        public const double DefaultMinValue = 0.0D;

        /// <summary>
        /// Default value for the <see cref="MaxValue"/> property.
        /// </summary>
        public const double DefaultMaxValue = 1.0D;

        /// <summary>
        /// Default value for the <see cref="MinHoldTime"/> property.
        /// </summary>
        public const double DefaultMinHoldTime = 2.0D;

        /// <summary>
        /// Default value for the <see cref="MaxHoldTime"/> property.
        /// </summary>
        public const double DefaultMaxHoldTime = 10.0D;

        /// <summary>
        /// Default value for the <see cref="MinMoveTime"/> property.
        /// </summary>
        public const double DefaultMinMoveTime = 1.0D;

        /// <summary>
        /// Default value for the <see cref="MaxMoveTime"/> property.
        /// </summary>
        public const double DefaultMaxMoveTime = 5.0D;

        /// <summary>
        /// Default value for the <see cref="PublishRate"/> property.
        /// </summary>
        public const double DefaultPublishRate = 30.0;

        /// <summary>
        /// Default value for the <see cref="ValueWraps"/> property.
        /// </summary>
        public const bool DefaultValueWraps = false;

        // Fields
        private double m_minValue;
        private double m_maxValue;
        private double m_minHoldTime;
        private double m_maxHoldTime;
        private double m_minMoveTime;
        private double m_maxMoveTime;
        private double m_publishRate;
        private bool m_valueWraps;

        private Timer m_timer;
        private double[] m_values;
        private double[] m_countdowns;
        private bool[] m_moveFlags;
        private long m_lastPublication;

        private double[] m_totalMoveTimes;
        private double[] m_accelerations;
        private double[] m_lastHoldValues;

        private bool m_disposed;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the smallest possible value generated by the adapter.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMinValue),
        Description("Defines the smallest possible value generated by the adapter.")]
        public double MinValue
        {
            get
            {
                return m_minValue;
            }
            set
            {
                m_minValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the largest possible value generated by the adapter.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMaxValue),
        Description("Defines the largest possible value generated by the adapter.")]
        public double MaxValue
        {
            get
            {
                return m_maxValue;
            }
            set
            {
                m_maxValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the smallest possible amount of time that the values will stay the same before moving again, in seconds.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMinHoldTime),
        Description("Defines the smallest possible amount of time that the values will stay the same before moving again, in seconds.")]
        public double MinHoldTime
        {
            get
            {
                return m_minHoldTime;
            }
            set
            {
                m_minHoldTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the smallest possible amount of time that the values will stay the same before moving again, in seconds.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMaxHoldTime),
        Description("Defines the largest possible amount of time that the values will stay the same before moving again, in seconds.")]
        public double MaxHoldTime
        {
            get
            {
                return m_maxHoldTime;
            }
            set
            {
                m_maxHoldTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the smallest possible amount of time that the values will move between two held values, in seconds.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMinMoveTime),
        Description("Defines the smallest possible amount of time that the values will move between two held values, in seconds.")]
        public double MinMoveTime
        {
            get
            {
                return m_minMoveTime;
            }
            set
            {
                m_minMoveTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the largest possible amount of time that the values will move between two held values, in seconds.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultMaxMoveTime),
        Description("Defines the largest possible amount of time that the values will move between two held values, in seconds.")]
        public double MaxMoveTime
        {
            get
            {
                return m_maxMoveTime;
            }
            set
            {
                m_maxMoveTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of values generated by the adapter per second.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultPublishRate),
        Description("Defines the number of values generated by the adapter per second.")]
        public double PublishRate
        {
            get
            {
                return m_publishRate;
            }
            set
            {
                m_publishRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the flag that determines whether the value can wrap from min to max or vice-versa.
        /// </summary>
        [ConnectionStringParameter,
        DefaultValue(DefaultValueWraps),
        Description("Defines the flag that determines whether the value can wrap from min to max or vice-versa.")]
        public bool ValueWraps
        {
            get
            {
                return m_valueWraps;
            }
            set
            {
                m_valueWraps = value;
            }
        }

        /// <summary>
        /// Gets the flag indicating if this adapter supports temporal processing.
        /// </summary>
        public override bool SupportsTemporalProcessing
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets flag that determines if the data input connects asynchronously.
        /// </summary>
        /// <remarks>
        /// Derived classes should return true when data input source is connects asynchronously, otherwise return false.
        /// </remarks>
        protected override bool UseAsyncConnect
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Initializes <see cref="MovingValueInputAdapter"/>.
        /// </summary>
        public override void Initialize()
        {
            Dictionary<string, string> settings;
            string setting;

            base.Initialize();
            settings = Settings;

            if (!settings.TryGetValue("minValue", out setting) || !double.TryParse(setting, out m_minValue))
                m_minValue = DefaultMinValue;

            if (!settings.TryGetValue("maxValue", out setting) || !double.TryParse(setting, out m_maxValue))
                m_maxValue = DefaultMaxValue;

            if (!settings.TryGetValue("minHoldTime", out setting) || !double.TryParse(setting, out m_minHoldTime))
                m_minHoldTime = DefaultMinHoldTime;

            if (!settings.TryGetValue("maxHoldTime", out setting) || !double.TryParse(setting, out m_maxHoldTime))
                m_maxHoldTime = DefaultMaxHoldTime;

            if (!settings.TryGetValue("minMoveTime", out setting) || !double.TryParse(setting, out m_minMoveTime))
                m_minMoveTime = DefaultMinMoveTime;

            if (!settings.TryGetValue("maxMoveTime", out setting) || !double.TryParse(setting, out m_maxMoveTime))
                m_maxMoveTime = DefaultMaxMoveTime;

            if (!settings.TryGetValue("publishRate", out setting) || !double.TryParse(setting, out m_publishRate))
                m_publishRate = DefaultPublishRate;

            if (settings.TryGetValue("valueWraps", out setting))
                m_valueWraps = setting.ParseBoolean();
            else
                m_valueWraps = DefaultValueWraps;

            if (m_minValue > m_maxValue)
                throw new InvalidOperationException(string.Format("minValue({0}) cannot be less than maxValue({1})", m_minValue, m_maxValue));

            if (m_minHoldTime > m_maxHoldTime)
                throw new InvalidOperationException(string.Format("minHoldTime({0}) cannot be less than maxHoldTime({1})", m_minHoldTime, m_maxHoldTime));

            if (m_minMoveTime > m_maxMoveTime)
                throw new InvalidOperationException(string.Format("minMoveTime({0}) cannot be less than maxMoveTime({1})", m_minMoveTime, m_maxMoveTime));

            if (m_maxHoldTime <= 0.0D)
                throw new InvalidOperationException(string.Format("maxHoldTime({0}) must be greater than zero", m_maxHoldTime));

            if (m_maxMoveTime <= 0.0D)
                throw new InvalidOperationException(string.Format("maxMoveTime({0}) must be greater than zero", m_maxMoveTime));

            if (m_publishRate <= 0.0D)
                throw new InvalidOperationException(string.Format("publishRate({0}) must be greater than zero", m_publishRate));
        }

        /// <summary>
        /// Gets a short one-line status of this <see cref="MovingValueInputAdapter"/>.
        /// </summary>
        /// <param name="maxLength">Maximum number of available characters for display.</param>
        /// <returns>
        /// A short one-line summary of the current status of this <see cref="MovingValueInputAdapter"/>.
        /// </returns>
        public override string GetShortStatus(int maxLength)
        {
            return string.Format("{0} values generated between {1} and {2} so far...", ProcessedMeasurements, m_minValue, m_maxValue).CenterText(maxLength);
        }

        /// <summary>
        /// Attempts to connect to data input source.
        /// </summary>
        protected override void AttemptConnection()
        {
            m_values = Enumerable.Repeat(m_maxValue - m_minValue, OutputMeasurements.Length)
                .Select(range => Generator.NextDouble() * range + m_minValue)
                .ToArray();

            m_countdowns = Enumerable.Repeat(m_maxHoldTime - m_minHoldTime, OutputMeasurements.Length)
                .Select(range => Generator.NextDouble() * range + m_minHoldTime)
                .ToArray();

            m_moveFlags = new bool[OutputMeasurements.Length];
            m_lastPublication = ToPublicationTime(DateTime.Now.Ticks);

            m_totalMoveTimes = new double[OutputMeasurements.Length];
            m_accelerations = new double[OutputMeasurements.Length];
            m_lastHoldValues = new double[OutputMeasurements.Length];

            using (Timer timer = m_timer)
            {
                m_timer = new Timer();
                m_timer.Interval = 1000.0D / m_publishRate;
                m_timer.Elapsed += Timer_Elapsed;
                m_timer.Start();
            }
        }

        /// <summary>
        /// Attempts to disconnect from data input source.
        /// </summary>
        protected override void AttemptDisconnection()
        {
            if ((object)m_timer != null)
            {
                m_timer.Elapsed -= Timer_Elapsed;
                m_timer.Stop();
                m_timer.Dispose();
                m_timer = null;
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="MovingValueInputAdapter"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        if ((object)m_timer != null)
                        {
                            m_timer.Elapsed -= Timer_Elapsed;
                            m_timer.Stop();
                            m_timer.Dispose();
                            m_timer = null;
                        }
                    }
                }
                finally
                {
                    m_disposed = true;          // Prevent duplicate dispose.
                    base.Dispose(disposing);    // Call base class Dispose().
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            long now = DateTime.Now.Ticks;
            long nextPublication = GetNextPublicationTime(m_lastPublication);
            double delta;

            while (nextPublication < now)
            {
                delta = Ticks.ToSeconds(nextPublication - m_lastPublication);

                for (int i = 0; i < OutputMeasurements.Length; i++)
                    Advance(i, delta);

                OnNewMeasurements(OutputMeasurements
                    .Select((measurement, index) => Measurement.Clone(measurement, GetWrappedValue(index), nextPublication))
                    .ToList<IMeasurement>());

                m_lastPublication = nextPublication;
                nextPublication = GetNextPublicationTime(m_lastPublication);
            }
        }

        private void Advance(int index, double time)
        {
            double remainingTime = time;

            while (remainingTime > 0.0D)
            {
                if (!m_moveFlags[index])
                    remainingTime = Hold(index, remainingTime);
                else
                    remainingTime = Move(index, remainingTime);

                if (remainingTime > 0.0D)
                    Switch(index);
            }
        }

        private double Hold(int index, double time)
        {
            double spentTime = Math.Min(time, m_countdowns[index]);
            double remainingTime = time - spentTime;
            m_countdowns[index] -= spentTime;
            return remainingTime;
        }

        private double Move(int index, double time)
        {
            double spentTime = Math.Min(time, m_countdowns[index]);
            double remainingTime = time - spentTime;

            double moveTime = m_totalMoveTimes[index] - m_countdowns[index] + spentTime;
            double accelerationTime = Math.Min(moveTime, m_totalMoveTimes[index] / 2.0D);
            double decelerationTime = moveTime - accelerationTime;

            double accelerationTerm = 0.5D * m_accelerations[index] * accelerationTime * accelerationTime;
            double decelerationTerm = 0.5D * m_accelerations[index] * decelerationTime * decelerationTime;
            double velocityTerm = m_accelerations[index] * accelerationTime * decelerationTime;

            m_values[index] = accelerationTerm - decelerationTerm + velocityTerm + m_lastHoldValues[index];
            m_countdowns[index] -= spentTime;

            return remainingTime;
        }

        private void Switch(int index)
        {
            double moveValue;
            double range;
            double min;

            m_moveFlags[index] = !m_moveFlags[index];

            if (!m_moveFlags[index])
            {
                m_values[index] = GetWrappedValue(index);
                range = m_maxHoldTime - m_minHoldTime;
                min = m_minHoldTime;
            }
            else
            {
                range = m_maxMoveTime - m_minMoveTime;
                min = m_minMoveTime;
            }

            m_countdowns[index] = Generator.NextDouble() * range + min;

            if (m_moveFlags[index])
            {
                range = m_maxValue - m_minValue;

                if (!m_valueWraps)
                    moveValue = Generator.NextDouble() * range + m_minValue;
                else
                    moveValue = (2.0D * Generator.NextDouble() * range) + (m_values[index] - range);


                m_totalMoveTimes[index] = m_countdowns[index];
                m_accelerations[index] = (moveValue - m_values[index]) / Math.Pow(m_totalMoveTimes[index] / 2, 2);
                m_lastHoldValues[index] = m_values[index];
            }
        }

        private long GetNextPublicationTime(long time)
        {
            double interval = Ticks.PerSecond / m_publishRate;
            long nextTime = (long)Math.Round(time + interval);
            return ToPublicationTime(nextTime);
        }

        private long ToPublicationTime(long time)
        {
            double interval = Ticks.PerSecond / m_publishRate;
            long seconds = time / Ticks.PerSecond;
            long subseconds = time % Ticks.PerSecond;
            long index = (long)Math.Round(subseconds / interval);
            return (seconds * Ticks.PerSecond) + (long)Math.Round(index * interval);
        }

        private double GetWrappedValue(int index)
        {
            if (m_valueWraps && m_values[index] != m_maxValue)
                return ToRange(m_values[index], m_minValue, m_maxValue - m_minValue);

            return m_values[index];
        }

        #endregion

        #region [ Static ]

        // Static Fields
        private static readonly Random Generator = new Random();

        // Static Methods
        private static double ToRange(double value, double minimum, double range)
        {
            double transform = value - minimum;
            double quotient = Math.Floor(transform / range);
            double remainder = transform - range * quotient;
            return remainder + minimum;
        }

        #endregion
    }
}
