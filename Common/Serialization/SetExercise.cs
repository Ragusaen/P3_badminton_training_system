using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetExerciseDescriptorRequest : PermissionRequest
    {
        public ExerciseDescriptor Exercise;
    }

    [Serializable, XmlRoot]
    public class SetExerciseDescriptorResponse : PermissionResponse
    {
        public ExerciseDescriptor Exercise;
    }
}
