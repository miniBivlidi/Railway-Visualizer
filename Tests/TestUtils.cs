using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    public static class TestUtils {
        public static void AreCollectionsEquivalent<T>(IEnumerable<T>[] actual, IEnumerable<T>[] expected) {
            Assert.That(actual.Length, Is.EqualTo(expected.Length));
            for(int i = 0; i < actual.Length; i++) {
                var hasEquivalentCollection = false;
                var resultSet = new HashSet<T>(actual[i]);
                for(int j = 0; j < expected.Length; j++) {
                    hasEquivalentCollection |= resultSet.SetEquals(expected[j]);
                }
                Assert.That(hasEquivalentCollection, Is.True);
            }
        }
    }
}
