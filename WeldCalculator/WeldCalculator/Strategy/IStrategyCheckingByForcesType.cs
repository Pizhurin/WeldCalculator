using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldCalculator.Strategy
{
    // Интерфейс реализует все возможные типы расчетов (необходимы конкретные стратегии типов соединений)
    // Для каждой стратегии будет своя реализация типов расчета на децствие N, M, Q, MQ, MN
    // AddParam добавляет необходимые параметры для конкретного типа соединения (конкретной стратегии)

    interface IStrategyCheckingByForcesType //IStrategyCalculateByForcesType
    {
        void AddParam(params string[] param);

        double CheckForceN();

        double CheckForceM();

        double CheckForceQ();

        double CheckForceMQ();

        double CheckForceMN();
        
    }
}
