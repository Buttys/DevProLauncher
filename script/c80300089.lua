--The First Monarch
function c80300089.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetTarget(c80300089.target)
	e1:SetOperation(c80300089.activate)
	c:RegisterEffect(e1)
	--change attribute
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80300089,1))
	e2:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetCode(EVENT_SPSUMMON_SUCCESS)
	e2:SetCost(c80300089.cost)
	e2:SetCondition(c80300089.con)
	e2:SetTarget(c80300089.tg)
	e2:SetOperation(c80300089.op)
	c:RegisterEffect(e2)
	--sum limit
	local e3=Effect.CreateEffect(c)
	e3:SetType(EFFECT_TYPE_FIELD)
	e3:SetRange(LOCATION_MZONE)
	e3:SetCode(EFFECT_CANNOT_SPECIAL_SUMMON)
	e3:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e3:SetTargetRange(1,0)
	e3:SetTarget(c80300089.splimit)
	c:RegisterEffect(e3)
end
function c80300089.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetLocationCount(tp,LOCATION_MZONE)>0 and 
		Duel.IsPlayerCanSpecialSummonMonster(tp,80300089,0,0x11,0,0,2,1000,2400,6,RACE_FIEND,ATTRIBUTE_DARKNESS) end
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,e:GetHandler(),1,0,0)
end
function c80300089.activate(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if not c:IsRelateToEffect(e) then return end
	if Duel.GetLocationCount(tp,LOCATION_MZONE)<=0
		or not Duel.IsPlayerCanSpecialSummonMonster(tp,80300089,0,0x11,1000,2400,6,RACE_FIEND,ATTRIBUTE_DARKNESS) then return end
	c:AddTrapMonsterAttribute(TYPE_EFFECT,ATTRIBUTE_DARKNESS,RACE_FIEND,6,1000,2400)
	Duel.SpecialSummon(c,1,tp,tp,true,false,POS_FACEUP)
	c:TrapMonsterBlock()
end
function c80300089.con(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():GetSummonType()==SUMMON_TYPE_SPECIAL+1
end
function c80300089.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(Card.IsDiscardable,tp,LOCATION_HAND,0,1,nil) end
	Duel.DiscardHand(tp,Card.IsDiscardable,1,1,REASON_COST+REASON_DISCARD)
end
function c80300089.tg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.Hint(HINT_SELECTMSG,tp,0)
	local aat=Duel.AnnounceAttribute(tp,1,0x7f)
	e:SetLabel(aat)
end
function c80300089.op(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local att=e:GetLabel()
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_SINGLE)
	e1:SetProperty(EFFECT_FLAG_COPY_INHERIT)
	e1:SetCode(EFFECT_CHANGE_ATTRIBUTE)
	e1:SetValue(att)
	e1:SetReset(RESET_EVENT+0x1fe0000)
	c:RegisterEffect(e1)
	--double tribute
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_SINGLE)
	e2:SetCode(EFFECT_DOUBLE_TRIBUTE)
	e2:SetValue(c80300089.condition)
	c:RegisterEffect(e2)
end
function c80300089.condition(e,c)
	return c:IsAttribute(e:GetHandler():GetAttribute())
end
function c80300089.splimit(e,c,tp,sumtp,sumpos)
	return not c80300089.condition(e,c)
end