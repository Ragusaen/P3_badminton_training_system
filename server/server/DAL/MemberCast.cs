﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class member
    {
        /// <summary>
        /// Checks if the member type int from the database is in the model type
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static void CheckMemberType(int dbType, Common.Model.MemberRole.Type modelType)
        {
            if ((dbType & (int) modelType) == 0)
                throw new InvalidCastException($"The member was not of type {modelType.ToString()}");
        }

        public static explicit operator Common.Model.Member(member m)
        {
            return new Member()
            {
                Id = m.ID,
                Name = m.Name
            };
        }

        public static explicit operator Common.Model.Player(member m)
        {
            CheckMemberType(m.MemberType, MemberRole.Type.Player);
            return new Player()
            {
                Sex = (Sex)m.Sex,
                BadmintonPlayerId = m.BadmintonPlayerID.GetValueOrDefault(),
                Member = (Common.Model.Member)m
            };
        }

        public static explicit operator Common.Model.Trainer(member m)
        {
            CheckMemberType(m.MemberType, MemberRole.Type.Trainer);
            return new Trainer()
            {
                Member = (Common.Model.Member)m
            };
        }
    }
}